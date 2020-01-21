using System;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Infrastructure.ConfigOptions
{
	public class JwtOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public DateTime IssuedAt => DateTime.UtcNow;
		public DateTime NotBefore => IssuedAt;
		public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(60);
		public DateTime Expiration => IssuedAt.Add(ValidFor);
		public SigningCredentials SigningCredentials { get; set; }
	}
}
