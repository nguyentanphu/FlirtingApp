using System.ComponentModel.DataAnnotations;

namespace FlirtingApp.Web.RequestModels
{
	public class LoginRequest
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
