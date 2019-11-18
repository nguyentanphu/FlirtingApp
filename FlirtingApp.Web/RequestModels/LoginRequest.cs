using System.ComponentModel.DataAnnotations;

namespace FlirtingApp.Web.RequestModels
{
	public class LoginRequest
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[StringLength(maximumLength: 10, MinimumLength = 4, ErrorMessage = "Password has to be bet 4 - 8 characters")]
		public string Password { get; set; }
	}
}
