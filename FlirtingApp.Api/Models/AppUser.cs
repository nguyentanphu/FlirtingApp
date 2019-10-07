using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Api.Models
{
	public class AppUser: IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
