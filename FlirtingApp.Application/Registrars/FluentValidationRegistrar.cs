using FlirtingApp.Application.Common.Behaviors;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FlirtingApp.Application.Registrars
{
	public static class FluentValidationRegistrar
	{
		public static IServiceCollection AddAppFluentValidation(this IServiceCollection services)
		{
			var assembliesToRegister = new[] { typeof(IAppDbContext).Assembly };
			AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair =>
			{
				services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
			});
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

			return services;
		} 
	}
}
