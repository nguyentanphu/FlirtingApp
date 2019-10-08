using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Data;
using FlirtingApp.Api.Dtos;
using FlirtingApp.Api.Identity;
using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Api.Services
{
	public class AuthService
	{
		private readonly ApiContext _apiContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly TokenFactory _tokenFactory;
		private readonly JwtFactory _jwtFactory;

		public AuthService(ApiContext apiContext, UserManager<AppUser> userManager, TokenFactory tokenFactory, JwtFactory jwtFactory)
		{
			_apiContext = apiContext;
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
			await _apiContext.SaveChangesAsync();

			var accessToken = _jwtFactory.GenerateEncodedTokens(currentUser.Id, currentUser.UserName);

			return new LoginReponse
			{
				RefreshToken = refreshToken,
				AccessToken = accessToken.Token,
				Success = true
			};
		}
	}
}
