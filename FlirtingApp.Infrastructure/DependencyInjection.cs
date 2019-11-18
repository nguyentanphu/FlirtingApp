using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Infrastructure.Identity;
using FlirtingApp.Infrastructure.Registras;
using FlirtingApp.Infrastructure.System;
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
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.GetName().Name)));

			services.AddScoped<IAppIdentityDbContext>(provider => provider.GetService<AppIdentityDbContext>());
			services.AddScoped<IAppUserManager, AppUserManager>();

			services.AddScoped<IMachineDateTime, MachineDateTime>();
			services.AddScoped<ITokenFactory, TokenFactory>();

			services.AddCustomJwtAuthentication(configuration);
			return services;
		}
	}
}
