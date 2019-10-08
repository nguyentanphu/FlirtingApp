using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Identity
{
	public class RefreshToken
	{
		private RefreshToken()
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
			AppUserId = userId;
			RemoteIpAddress = remoteIpAddress;
			Expires = expires;
		}

		public Guid Id { get; set; }
		public string Token { get; private set; }
		public DateTime Expires { get; private set; }
		public Guid AppUserId { get; private set; }
		public bool Active => DateTime.UtcNow <= Expires;
		public string RemoteIpAddress { get; private set; }
	}
}
