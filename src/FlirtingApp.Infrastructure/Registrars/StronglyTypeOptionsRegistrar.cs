﻿using System.Text;
using FlirtingApp.Application.Utils;
using FlirtingApp.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Infrastructure.Registrars
{
	public static class StronglyTypeOptionsRegistrar
	{
		public static IServiceCollection AddStronglyTypeOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(sp => configuration.GetOptions<CloudinaryCredential>(nameof(CloudinaryCredential)));
			return services;
		}
	}
}
