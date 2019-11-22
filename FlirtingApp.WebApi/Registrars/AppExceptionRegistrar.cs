using FlirtingApp.Web.Middleware;
using Microsoft.AspNetCore.Builder;

namespace FlirtingApp.Web.Registrars
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
