using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Infrastructure.Exceptions;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	class SecurityUserManager : ISecurityUserManager
	{
		private readonly UserManager<SecurityUser> _userManager;
		private readonly IdentityDbContext _identityDbContext;
		private readonly ITokenFactory _tokenFactory;
		public SecurityUserManager(UserManager<SecurityUser> userManager, IdentityDbContext identityDbContext, ITokenFactory tokenFactory)
		{
			_userManager = userManager;
			_identityDbContext = identityDbContext;
			_tokenFactory = tokenFactory;
		}

		public async Task<bool> UserNameExistAsync(string userName)
		{
			return await _identityDbContext.AppUsers.AnyAsync(a => a.UserName == userName);
		}
		public async Task<Guid> CreateUserAsync(string userName, string password)
		{
			var newUser = new SecurityUser
			{
				UserName = userName,
			};
			var result = await _userManager.CreateAsync(newUser, password);
			if (!result.Succeeded)
			{
				throw new CreateAppUserException(string.Join(",", result.Errors.Select(e => e.Description)));
			}

			return newUser.Id;
		}

		public async Task<Result<(Guid SecurityUserId, string RefreshToken)>> LoginUserAsync(string userName, string password, string remoteIpAddress)
		{
			var matchedUser = await _userManager.FindByNameAsync(userName);
			if (matchedUser == null)
			{
				return Result.Fail<(Guid SecurityUserId, string RefreshToken)>("Cannot find user");
			}

			var valid = await _userManager.CheckPasswordAsync(matchedUser, password);
			if (!valid)
			{
				return Result.Fail<(Guid SecurityUserId, string RefreshToken)>("Password does not match");
			}

			var refreshToken = _tokenFactory.GenerateToken();
			matchedUser.AddRefreshToken(refreshToken, remoteIpAddress);
			await _identityDbContext.SaveChangesAsync();

			return Result.Ok((matchedUser.Id, refreshToken));
		}

		public async Task LogoutUserAsync(Guid securityUserId, string remoteIpAddress)
		{
			var matchedUser = await _identityDbContext.AppUsers
				.Include(a => a.RefreshTokens)
				.FirstAsync(a => a.Id == securityUserId);
			matchedUser.RemoveRefreshToken(remoteIpAddress);

			await _identityDbContext.SaveChangesAsync();
		}
		public async Task<bool> HasValidRefreshTokenAsync(string refreshToken, Guid securityUserId, string remoteIpAddress)
		{
			var matchedUser = await _identityDbContext.AppUsers
				.Include(a => a.RefreshTokens)
				.FirstAsync(a => a.Id == securityUserId);
			return matchedUser.HasValidRefreshToken(refreshToken, remoteIpAddress);
		}

		public async Task<string> ExchangeRefreshTokenAsync(Guid securityUserId, string refreshToken, string remoteIpAddress)
		{
			var appUser = await _identityDbContext.AppUsers
				.Include(a => a.RefreshTokens)
				.FirstAsync(a => a.Id == securityUserId);

			if (!appUser.HasValidRefreshToken(refreshToken, remoteIpAddress))
			{
				throw new InvalidRefreshTokenException();
			}

			appUser.RemoveRefreshToken(refreshToken);
			var newRefreshToken = _tokenFactory.GenerateToken();
			appUser.AddRefreshToken(newRefreshToken, remoteIpAddress);
			await _identityDbContext.SaveChangesAsync();

			return newRefreshToken;
		}

	}
}
