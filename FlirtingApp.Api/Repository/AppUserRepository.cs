using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Data;
using FlirtingApp.Api.Identity;
using FlirtingApp.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Api.Repository
{
	public class AppUserRepository
	{
		private readonly ApiContext _apiContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly TokenFactory _tokenFactory;

		public AppUserRepository(
			UserManager<AppUser> userManager, 
			ApiContext apiContext, 
			TokenFactory tokenFactory)
		{
			_userManager = userManager;
			_apiContext = apiContext;
			_tokenFactory = tokenFactory;
		}

		public async Task<AppUser> Create(string firstName, string lastName, string email, string userName,
			string password)
		{
			var newUser = new AppUser
			{
				Email = email,
				UserName = userName,
				FirstName = firstName,
				LastName = lastName
			};

			var result = await _userManager.CreateAsync(newUser, password);
			if (!result.Succeeded)
			{
				throw new Exception("Could not create new user");
			}

			return newUser;
		}

		public async Task<bool> Login(string userName, string password, string remoteIpAdress)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
			{
				return false;
			}

			var currentUser = await _userManager.FindByNameAsync(userName);
			if (currentUser == null)
			{
				return false;
			}

			var passwordMatched = await _userManager.CheckPasswordAsync(currentUser, password);
			if (!passwordMatched)
			{
				return false;
			}

			var refreshToken = _tokenFactory.GenerateToken();
			currentUser.AddRefreshToken(refreshToken, currentUser.Id, remoteIpAdress);
			await _apiContext.SaveChangesAsync();

			return false;
		}
	}
}
