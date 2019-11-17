using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlirtingApp.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppIdentityDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(AppIdentityDbContext).AssemblyQualifiedName)));

			return services;
		}
	}
}
