using System;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Infrastructure.Identity.Models
{
	public class RefreshToken: IIdentifiable
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

		public Guid Id { get; set; }
		public string Token { get; set; }
		public DateTime Expires { get; set; }
		public Guid SecurityUserId { get; set; }
		public SecurityUser SecurityUser { get; set; }
		public bool Active => DateTime.UtcNow <= Expires;
		public string RemoteIpAddress { get; set; }
	}
}
