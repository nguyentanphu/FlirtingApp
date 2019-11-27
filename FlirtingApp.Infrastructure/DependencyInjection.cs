using System.IdentityModel.Tokens.Jwt;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;
using FlirtingApp.Infrastructure.Identity;
using FlirtingApp.Infrastructure.Registrars;
using FlirtingApp.Infrastructure.System;
using FlirtingApp.Infrastructure.ThirdPartyVendor;
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
			services.AddScoped<ITokenFactory, TokenFactory>();
			services.AddScoped<JwtSecurityTokenHandler>();
			services.AddScoped<IJwtFactory, JwtFactory>();

			services.AddScoped<IMachineDateTime, MachineDateTime>();
			services.AddScoped<ICloudinary, CloudinaryAdapter>();

			services.AddAppJwtAuthentication(configuration);

			services.AddStronglyTypeOptions(configuration);
			return services;
		}
	}
}
