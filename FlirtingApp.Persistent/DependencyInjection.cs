using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlirtingApp.Persistent
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistent(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(AppDbContext).AssemblyQualifiedName)));

			services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
			return services;
		}
	}
}
