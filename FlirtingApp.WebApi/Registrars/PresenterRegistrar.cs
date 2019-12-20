using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.WebApi.ApiPresenters;
using Microsoft.Extensions.DependencyInjection;

namespace FlirtingApp.WebApi.Registrars
{
	public static class PresenterRegistrar
	{
		public static IServiceCollection AddPresenters(this IServiceCollection services)
		{
			services.AddSingleton<LoginPresenter>();

			return services;
		}
	}
}
