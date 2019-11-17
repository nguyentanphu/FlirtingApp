using System;

namespace FlirtingApp.Web.Identity
{
	public class RefreshToken
	{
		internal RefreshToken()
		{

		}
		public RefreshToken(
			string token, 
			Guid userId, 
			string remoteIpAddress, 
			DateTime expires
		)
		{
			Token = token;
			UserId = userId;
			RemoteIpAddress = remoteIpAddress;
			Expires = expires;
		}

		public Guid Id { get; set; }
		public string Token { get; set; }
		public DateTime Expires { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
		public bool Active => DateTime.UtcNow <= Expires;
		public string RemoteIpAddress { get; set; }
	}
}
