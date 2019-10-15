using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Data;
using FlirtingApp.Api.Identity;
using Newtonsoft.Json;

namespace FlirtingApp.Api.SeedData
{
	public class Seed
	{
		public static void SeedUsers(ApiDbContext _context)
		{
			if (_context.Users.Any())
			{
				return;
			}

			var usersJson = File.ReadAllText("SeedData/seedData.json");
			var userList = JsonConvert.DeserializeObject<List<User>>(usersJson);

		}
	}
}
