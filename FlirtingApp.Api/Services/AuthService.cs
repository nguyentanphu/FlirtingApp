using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Api.ConfigOptions;
using FlirtingApp.Api.Data;
using FlirtingApp.Api.Dtos;
using FlirtingApp.Api.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Api.Services
{
	public class AuthService
	{
		private readonly ApiDbContext _apiDbContext;
		private readonly UserManager<User> _userManager;
		private readonly TokenFactory _tokenFactory;
		private readonly JwtFactory _jwtFactory;

		public AuthService(
			ApiDbContext apiDbContext, 
			UserManager<User> userManager, 
			TokenFactory tokenFactory, 
			JwtFactory jwtFactory)
		{
			_apiDbContext = apiDbContext;
			_userManager = userManager;
			_tokenFactory = tokenFactory;
			_jwtFactory = jwtFactory;
		}

		public async Task<LoginReponse> Login(string userName, string password, string remoteIpAdress)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
			{
				return new LoginReponse
				{
					Success = false
				};
			}

			var currentUser = await _userManager.FindByNameAsync(userName);
			if (currentUser == null)
			{
				return new LoginReponse
				{
					Success = false
				};
			}

			var passwordMatched = await _userManager.CheckPasswordAsync(currentUser, password);
			if (!passwordMatched)
			{
				return new LoginReponse
				{
					Success = false
				};
			}

			var refreshToken = _tokenFactory.GenerateToken();
			currentUser.AddRefreshToken(refreshToken, currentUser.Id, remoteIpAdress);
			await _apiDbContext.SaveChangesAsync();

			var accessToken = _jwtFactory.GenerateEncodedTokens(currentUser.Id, currentUser.UserName);

			return new LoginReponse
			{
				RefreshToken = refreshToken,
				AccessToken = accessToken.Token,
				Success = true
			};
		}

		public async Task<LoginReponse> ExchangeRefreshToken(
			string accessToken, 
			string refreshToken, 
			string jwtSecret, 
			string remoteIpAdress
		)
		{
			var claimPrincipal = _jwtFactory.GetClaimPrinciple(accessToken, new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)));
			var userIdClaim = claimPrincipal.FindFirst(c => c.Type == "id");
			var currentUser = await _apiDbContext.Users.Include(u => u.RefreshTokens)
				.FirstAsync(u => u.Id == new Guid(userIdClaim.Value));
			if (!currentUser.HasValidRefreshToken(refreshToken))
			{
				return new LoginReponse
				{
					Success = false
				};
			}

			var newAccessToken = _jwtFactory.GenerateEncodedTokens(currentUser.Id, currentUser.UserName);
			var newRefreshToken = _tokenFactory.GenerateToken();
			currentUser.RemoveRefreshToken(refreshToken);
			currentUser.AddRefreshToken(newRefreshToken, currentUser.Id, remoteIpAdress);
			await _apiDbContext.SaveChangesAsync();
			return new LoginReponse
			{
				RefreshToken = newRefreshToken,
				AccessToken = newAccessToken.Token,
				Success = true
			};
		}
	}
}
