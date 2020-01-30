using FlirtingApp.Application;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Infrastructure;
using FlirtingApp.Persistent;
using FlirtingApp.WebApi.Controllers;
using FlirtingApp.WebApi.HostedServices;
using FlirtingApp.WebApi.Registrars;
using FlirtingApp.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FlirtingApp.WebApi
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

			// For run migration on app start and seed users and photos data
			services.AddHostedService<MigrationHostedService>();

			services.AddControllers();

			services.AddCors();

			services.AddSwaggerWithBearerToken();

			services.AddPresenters();

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

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();

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
				endpoints.MapFallbackToController(nameof(HomeController.Index), "Home");
			});
		}
	}
}
