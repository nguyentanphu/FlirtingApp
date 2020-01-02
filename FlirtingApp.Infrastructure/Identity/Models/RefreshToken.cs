using System;
using System.Collections.Generic;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Infrastructure.Identity.Models
{
	public class RefreshToken: ValueObject<RefreshToken>
	{
		private RefreshToken()
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

		public string Token { get; private set; }
		public DateTime Expires { get; private set; }
		public bool Active => DateTime.UtcNow <= Expires;
		public string RemoteIpAddress { get; private set; }
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Token;
			yield return RemoteIpAddress;
			yield return Expires;
		}
	}
}
