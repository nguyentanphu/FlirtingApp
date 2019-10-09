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
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FlirtingApp.Api.Identity
{
	public class JwtFactory
	{
		private readonly JwtOptions _jwtOptions;
		private readonly JwtSecurityTokenHandler _jwtHandler;

		public JwtFactory(IOptions<JwtOptions> jwtOptions, JwtSecurityTokenHandler jwtHandler)
		{
			_jwtHandler = jwtHandler;
			_jwtOptions = jwtOptions.Value;
		}

		public AccessToken GenerateEncodedTokens(Guid id, string userName)
		{
			var identity = GenerateClaimIdentity(id, userName);
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				identity.FindFirst("rol"),
				identity.FindFirst("id")
			};

			var jwt = new JwtSecurityToken(
				new JwtHeader(_jwtOptions.SigningCredentials), 
				new JwtPayload(_jwtOptions.Issuer, _jwtOptions.Audience, claims, _jwtOptions.NotBefore, _jwtOptions.Expiration, _jwtOptions.IssuedAt)
			);

			return new AccessToken(_jwtHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
		}

		public ClaimsPrincipal GetClaimPrinciple(string accessToken, SecurityKey signingKey)
		{
			return _jwtHandler.ValidateToken(accessToken, new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,
				ValidateLifetime = false,
			}, out var validatedToken);
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
