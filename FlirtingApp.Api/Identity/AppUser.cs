using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Api.Identity
{
	public class AppUser: IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
