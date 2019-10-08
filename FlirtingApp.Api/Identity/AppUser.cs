using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Api.Identity
{
	public class AppUser: IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
		public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

		public void AddRefreshToken(string token, Guid userId, string remoteIpAddress, double daysToExpire = 5)
		{
			_refreshTokens.Add(new RefreshToken(token, userId, remoteIpAddress, DateTime.UtcNow.AddDays(daysToExpire)));
		}
	}
}
