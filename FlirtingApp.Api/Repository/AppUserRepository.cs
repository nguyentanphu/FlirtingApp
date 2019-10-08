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
		private readonly UserManager<User> _userManager;
		private readonly TokenFactory _tokenFactory;

		public AppUserRepository(
			UserManager<User> userManager, 
			ApiContext apiContext, 
			TokenFactory tokenFactory)
		{
			_userManager = userManager;
			_apiContext = apiContext;
			_tokenFactory = tokenFactory;
		}

		public async Task<User> Create(string firstName, string lastName, string email, string userName,
			string password)
		{
			var newUser = new User
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

		
	}
}
