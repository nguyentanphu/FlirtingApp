using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FlirtingApp.Web.Registras
{
	public static class AppExceptionRegistra
	{
		public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<AppExceptionMiddleware>();
			return builder;
		}

	}
}
