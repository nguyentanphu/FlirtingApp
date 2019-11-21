using System.IdentityModel.Tokens.Jwt;
using FlirtingApp.Application;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Infrastructure;
using FlirtingApp.Persistent;
using FlirtingApp.Web.HostedServices;
using FlirtingApp.Web.Identity;
using FlirtingApp.Web.Registrars;
using FlirtingApp.Web.Repository;
using FlirtingApp.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlirtingApp.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddInfrastructure(Configuration);
			services.AddPersistent(Configuration);
			services.AddApplication();
			services.AddHttpContextAccessor();
			services.AddScoped<ICurrentUser, CurrentUserService>();

			services.AddHostedService<MigrationHostedService>();

			services.AddControllers();

			services.AddCors();

			services.AddSwaggerWithBearerToken();

			//services.AddAutoMapper(this.GetType().Assembly);

			services.AddScoped<JwtSecurityTokenHandler>();
			services.AddScoped<UserRepository>();
			services.AddScoped<AuthService>();
			services.AddScoped<TokenFactory>();
			services.AddScoped<JwtFactory>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseAppExceptionHandler();
			}

			app.UseHttpsRedirection();

			app.UseSwaggerWithUI();

			app.UseRouting();
			app.UseCors(config =>
			{
				config.AllowAnyOrigin();
				config.AllowAnyHeader();
				config.AllowAnyMethod();
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
