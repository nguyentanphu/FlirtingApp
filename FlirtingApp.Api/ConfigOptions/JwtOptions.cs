using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Api.ConfigOptions
{
	public class JwtOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public DateTime IssuedAt => DateTime.UtcNow;
		public DateTime NotBefore => IssuedAt;
		public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(1);
		public DateTime Expiration => IssuedAt.Add(ValidFor);
		public SigningCredentials SigningCredentials { get; set; }
	}
}
