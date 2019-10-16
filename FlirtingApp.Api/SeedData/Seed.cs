using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Data;
using FlirtingApp.Api.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FlirtingApp.Api.SeedData
{
	public static class Seed
	{
		public static IWebHost MigrateAndSeedDatabase(this IWebHost webHost)
		{
			using (var scope = webHost.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetService<ApiDbContext>();
				var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

				dbContext.Database.Migrate();
				SeedUsers(dbContext, userManager);
			}

			return webHost;
		}
		public static void SeedUsers(ApiDbContext context, UserManager<User> userManager)
		{
			if (context.Users.Any())
			{
				return;
			}

			var usersJson = File.ReadAllText("SeedData/seedData.json");
			var userList = JsonConvert.DeserializeObject<List<User>>(usersJson);
			foreach (var user in userList)
			{
				userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
			}
		}
	}
}
