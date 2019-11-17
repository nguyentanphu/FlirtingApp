using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Infrastructure.Exceptions;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	class AppAppUserManager : IAppUserManager
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly AppIdentityDbContext _identityDbContext;
		public AppAppUserManager(UserManager<AppUser> userManager, AppIdentityDbContext identityDbContext)
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

		public async Task<bool> HasValidRefreshToken(string refreshToken, Guid appUserId, string remoteIpAddress)
		{
			var matchedUser = await _identityDbContext.AppUsers
				.Include(a => a.RefreshTokens)
				.FirstAsync(a => a.Id == appUserId);
			return matchedUser.HasValidRefreshToken(refreshToken, remoteIpAddress);
		}

		public async Task MigrateIdentityDb()
		{
			await _identityDbContext.Database.MigrateAsync();
		}
	}
}
