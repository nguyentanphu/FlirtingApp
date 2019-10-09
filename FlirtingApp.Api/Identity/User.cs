using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace FlirtingApp.Api.Identity
{
	public class User: IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		private readonly HashSet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
		public IEnumerable<RefreshToken> RefreshTokens => _refreshTokens.ToList();

		public void AddRefreshToken(string token, Guid userId, string remoteIpAddress, double daysToExpire = 5)
		{
			_refreshTokens.Add(new RefreshToken(token, userId, remoteIpAddress, DateTime.UtcNow.AddDays(daysToExpire)));
		}

		public void RemoveAllRefreshToken(string refreshToken)
		{
			_refreshTokens.Remove(RefreshTokens.First(t => t.Token == refreshToken));

		}

		public bool HasValidRefreshToken(string refreshToken)
		{
			return _refreshTokens.Any(t => t.Token == refreshToken && t.Active);
		}
	}
}
