using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using FlirtingApp.Api.ConfigOptions;
using FlirtingApp.Api.Dtos;
using Microsoft.Extensions.Options;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FlirtingApp.Api.Identity
{
	public class JwtFactory
	{
		private readonly JwtOptions _jwtOptions;
		private readonly JwtSecurityTokenHandler _jwtHandler;

		public JwtFactory(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}

		public AccessToken GenerateEncodedTokens(Guid id, string userName)
		{
			var identity = GenerateClaimIdentity(id, userName);
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userName),
				new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat,
					((DateTimeOffset) _jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString()),
				identity.FindFirst("rol"),
				identity.FindFirst("id")
			};

			var jwt = new JwtSecurityToken(
				new JwtHeader(_jwtOptions.SigningCredentials), 
				new JwtPayload(_jwtOptions.Issuer, _jwtOptions.Audience, claims, _jwtOptions.NotBefore, _jwtOptions.Expiration)
			);

			return new AccessToken(_jwtHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
		}

		private ClaimsIdentity GenerateClaimIdentity(Guid id, string userName)
		{
			return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new Claim[] 
			{
				new Claim("id", id.ToString()),
				new Claim("rol", "api_access")
			});
		}
	}
}
