﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Infrastructure.Exceptions;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	class AppUserManager : IAppUserManager
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly AppIdentityDbContext _identityDbContext;
		public AppUserManager(UserManager<AppUser> userManager, AppIdentityDbContext identityDbContext)
		{
			_userManager = userManager;
			_identityDbContext = identityDbContext;
		}

		public async Task<Guid> CreateUserAsync(string userName, string password)
		{
			var newUser = new AppUser
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

		public async Task<(bool Success, Guid UserId)> LoginUserAsync(string userName, string password, string refreshToken, string remoteIpAddress)
		{
			var matchedUser = await _userManager.FindByNameAsync(userName);
			if (matchedUser == null)
			{
				return (false, default);
			}

			var valid = await _userManager.CheckPasswordAsync(matchedUser, password);
			if (!valid)
			{
				return (false, default);
			}

			matchedUser.AddRefreshToken(refreshToken, remoteIpAddress);
			await _identityDbContext.SaveChangesAsync();
			return (true, matchedUser.Id);
		}

		public async Task<bool> HasValidRefreshTokenAsync(string refreshToken, Guid appUserId, string remoteIpAddress)
		{
			var matchedUser = await _identityDbContext.AppUsers
				.Include(a => a.RefreshTokens)
				.FirstAsync(a => a.Id == appUserId);
			return matchedUser.HasValidRefreshToken(refreshToken, remoteIpAddress);
		}

	}
}