using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using FlirtingApp.Application.Common.Behaviors;
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
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
