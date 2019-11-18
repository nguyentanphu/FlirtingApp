using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Infrastructure.ConfigOptions;
using FlirtingApp.Infrastructure.Identity;
using FlirtingApp.Infrastructure.Identity.Models;
using FlirtingApp.Infrastructure.Registras;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppIdentityDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(AppIdentityDbContext).AssemblyQualifiedName)));

			services.AddScoped<IAppIdentityDbContext>(provider => provider.GetService<AppIdentityDbContext>());
			services.AddScoped<IAppUserManager, AppAppUserManager>();

			services.AddCustomJwtAuthentication(configuration);
			return services;
		}
	}
}
