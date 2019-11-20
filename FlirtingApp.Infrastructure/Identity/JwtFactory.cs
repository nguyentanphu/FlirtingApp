using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FlirtingApp.Infrastructure.Identity
{
	public class JwtFactory : IJwtFactory
	{
		private readonly JwtOptions _jwtOptions;
		private readonly JwtAuthOptions _jwtAuthOptions;
		private readonly JwtSecurityTokenHandler _jwtHandler;

		public JwtFactory(IOptions<JwtOptions> jwtOptions, IOptions<JwtAuthOptions> jwtAuthOptions, JwtSecurityTokenHandler jwtHandler)
		{
			_jwtHandler = jwtHandler;
			_jwtAuthOptions = jwtAuthOptions.Value;
			_jwtOptions = jwtOptions.Value;
		}

		public string GenerateEncodedTokens(Guid userId, Guid appUserId, string userName)
		{
			var claims = new[]
			{
				new Claim(AppClaimTypes.UserId, userId.ToString()),
				new Claim(AppClaimTypes.AppUserId, appUserId.ToString()),
				new Claim(AppClaimTypes.Role, "api_access"),
				new Claim(AppClaimTypes.Sub, userName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var jwt = new JwtSecurityToken(
				new JwtHeader(_jwtOptions.SigningCredentials), 
				new JwtPayload(_jwtOptions.Issuer, _jwtOptions.Audience, claims, _jwtOptions.NotBefore, _jwtOptions.Expiration, _jwtOptions.IssuedAt)
			);

			return _jwtHandler.WriteToken(jwt);
		}

		public ClaimsPrincipal GetClaimPrinciple(string accessToken)
		{
			try
			{
				return _jwtHandler.ValidateToken(accessToken, new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtAuthOptions.Secret)),
					ValidateLifetime = false,
				}, out _);
			}
			catch (Exception e)
			{
				throw new InvalidJwtException(innerException: e);
			}
		}

	}
}
