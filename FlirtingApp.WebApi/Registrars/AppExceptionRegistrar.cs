using FlirtingApp.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;

namespace FlirtingApp.WebApi.Registrars
{
	public static class AppExceptionRegistrar
	{
		public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<AppExceptionMiddleware>();
			return builder;
		}

	}
}
