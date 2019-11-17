namespace FlirtingApp.Web.Dtos
{
	public class LoginReponse
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public bool Success { get; set; }
	}
}
