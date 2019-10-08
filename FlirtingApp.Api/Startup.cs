using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FlirtingApp.Api.ConfigOptions;
using FlirtingApp.Api.Data;
using FlirtingApp.Api.Identity;
using FlirtingApp.Api.Repository;
using FlirtingApp.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FlirtingApp.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var authSettings = Configuration.GetSection(nameof(AuthOptions));
			var jwtOptions = Configuration.GetSection(nameof(JwtOptions));
			var signingKey =
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthOptions.JwtSecret)]));
			services.Configure<JwtOptions>(options =>
			{
				options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
				options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
				options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			});
			services.Configure<AuthOptions>(Configuration.GetSection(nameof(AuthOptions)));

			// add identity
			var identityBuilder = services.AddDefaultIdentity<User>(o =>
			{
				// configure identity options
				o.Password.RequireDigit = false;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 6;
			}).AddEntityFrameworkStores<ApiContext>();

			services.AddCors();

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

			services.AddScoped<JwtSecurityTokenHandler>();
			services.AddScoped<AppUserRepository>();
			services.AddScoped<AuthService>();
			services.AddScoped<TokenFactory>();
			services.AddScoped<JwtFactory>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseCors(config =>
			{
				config.AllowAnyOrigin();
				config.AllowAnyHeader();
				config.AllowAnyMethod();
			});
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
