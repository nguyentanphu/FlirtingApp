using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FlirtingApp.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FlirtingApp.Infrastructure.Identity
{
	public interface IJwtFactory
	{
		string GenerateEncodedTokens(Guid id, string userName);
		ClaimsPrincipal GetClaimPrinciple(string accessToken, SecurityKey signingKey);
	}

	public class JwtFactory : IJwtFactory
	{
		private readonly JwtOptions _jwtOptions;
		private readonly JwtSecurityTokenHandler _jwtHandler;

		public JwtFactory(IOptions<JwtOptions> jwtOptions, JwtSecurityTokenHandler jwtHandler)
		{
			_jwtHandler = jwtHandler;
			_jwtOptions = jwtOptions.Value;
		}

		public string GenerateEncodedTokens(Guid id, string userName)
		{
			var claims = new[]
			{
				new Claim("id", id.ToString()),
				new Claim("rol", "api_access"),
				new Claim(JwtRegisteredClaimNames.Sub, userName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var jwt = new JwtSecurityToken(
				new JwtHeader(_jwtOptions.SigningCredentials), 
				new JwtPayload(_jwtOptions.Issuer, _jwtOptions.Audience, claims, _jwtOptions.NotBefore, _jwtOptions.Expiration, _jwtOptions.IssuedAt)
			);

			return _jwtHandler.WriteToken(jwt);
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
			}, out _);
		}

	}
}
