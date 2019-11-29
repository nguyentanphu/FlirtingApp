using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Infrastructure.Identity.Models
{
	public class AppUser : IdentityUser<Guid>
	{
		private readonly HashSet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
		public IEnumerable<RefreshToken> RefreshTokens => _refreshTokens.ToList();


		public void AddRefreshToken(string token, string remoteIpAddress, double daysToExpire = 5)
		{
			_refreshTokens.Add(new RefreshToken(token, remoteIpAddress, DateTime.UtcNow.AddDays(daysToExpire)));
		}

		public void RemoveRefreshToken(string remoteIpAddress)
		{
			_refreshTokens.RemoveWhere(r => r.RemoteIpAddress == remoteIpAddress);
		}

		public bool HasValidRefreshToken(string refreshToken, string remoteIpAddress)
		{
			return _refreshTokens.Any(t => t.Token == refreshToken && t.RemoteIpAddress == remoteIpAddress && t.Active);
		}
	}
}
