using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Api.Repository
{
	public class AppUserRepository
	{
		private readonly UserManager<AppUser> _userManager;

		public AppUserRepository(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
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
	}
}
