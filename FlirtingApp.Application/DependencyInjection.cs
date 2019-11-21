using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FlirtingApp.Application.Registrars;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FlirtingApp.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			//services.AddFluentValidation(option => option.RegisterValidatorsFromAssembly(typeof(IAppDbContext).Assembly))
			services.AddAppFluentValidation();
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
