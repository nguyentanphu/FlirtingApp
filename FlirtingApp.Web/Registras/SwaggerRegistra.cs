using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FlirtingApp.Web.Registras
{
	public static class SwaggerRegistra
	{
		public static IServiceCollection AddSwaggerWithBearerToken(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please insert JWT with Bearer into field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{ new OpenApiSecurityScheme
						{
							In = ParameterLocation.Header,
							Description = "Please insert JWT with Bearer into field",
							Name = "Authorization",
							Type = SecuritySchemeType.ApiKey

						}, new string[] { }
					}
				});
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder appBuilder)
		{
			appBuilder.UseSwagger();
			appBuilder.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			return appBuilder;
		}
	}
}
