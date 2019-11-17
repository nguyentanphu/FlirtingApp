using System.ComponentModel.DataAnnotations;

namespace FlirtingApp.Web.RequestModels
{
	public class RegisterUserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required]
		public string UserName { get; set; }
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
