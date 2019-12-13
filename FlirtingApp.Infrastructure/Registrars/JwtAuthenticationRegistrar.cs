using System;
using System.Text;
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
			var authSettings = configuration.GetSection(nameof(JwtAuthOptions));
			var jwtOptions = configuration.GetSection(nameof(JwtOptions));
			var signingKey =
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(JwtAuthOptions.Secret)]));
			services.Configure<JwtOptions>(options =>
			{
				options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
				options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
				options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);
			});
			services.Configure<JwtAuthOptions>(authSettings);

			services.AddIdentityCore<SecurityUser>(o =>
			{
				// configure identity options
				o.Password.RequireDigit = false;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 6;
			}).AddEntityFrameworkStores<AppIdentityDbContext>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],
					ValidateAudience = true,
					ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = signingKey,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};

				options.ClaimsIssuer = jwtOptions[nameof(JwtOptions.Issuer)];
				options.TokenValidationParameters = tokenValidationParameters;
			});

			return services;
		}
	}
}
