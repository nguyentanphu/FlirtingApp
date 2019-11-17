using System.ComponentModel.DataAnnotations;

namespace FlirtingApp.Web.RequestModels
{
	public class RefreshTokenRequest
	{
		[Required]
		public string AccessToken { get; set; }
		[Required]
		public string RefreshToken { get; set; }
	}
}
