using System;

namespace FlirtingApp.Infrastructure.Identity.Models
{
	public class RefreshToken
	{
		internal RefreshToken()
		{

		}
		public RefreshToken(
			string token, 
			string remoteIpAddress, 
			DateTime expires
		)
		{
			Token = token;
			RemoteIpAddress = remoteIpAddress;
			Expires = expires;
		}

		public Guid RefreshTokenId { get; set; }
		public string Token { get; set; }
		public DateTime Expires { get; set; }
		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public bool Active => DateTime.UtcNow <= Expires;
		public string RemoteIpAddress { get; set; }
	}
}
