using System;
using System.Text;
using FlirtingApp.Application.Utils;
using FlirtingApp.Infrastructure.ConfigOptions;
using FlirtingApp.Infrastructure.Identity;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Infrastructure.Registrars
{
	public static class JwtAuthenticationRegistrar
	{
		public static IServiceCollection AddAppJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var authSettings = configuration.GetOptions<JwtAuthOptions>(nameof(JwtAuthOptions));
			services.AddSingleton<JwtAuthOptions>(authSettings);
			var signingKey =
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Secret));

			var jwtOptions = configuration.GetOptions<JwtOptions>(nameof(JwtOptions));
			services.AddSingleton<JwtOptions>(sp =>
			{
				jwtOptions.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);
				return jwtOptions;
			});
			
			services.AddIdentityCore<SecurityUser>(o =>
			{
				// configure identity options
				o.Password.RequireDigit = false;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 6;
			}).AddEntityFrameworkStores<IdentityDbContext>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtOptions.Issuer,
					ValidateAudience = true,
					ValidAudience = jwtOptions.Audience,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = signingKey,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};

				options.ClaimsIssuer = jwtOptions.Issuer;
				options.TokenValidationParameters = tokenValidationParameters;
			});

			return services;
		}
	}
}
