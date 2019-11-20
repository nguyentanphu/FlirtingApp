using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FlirtingApp.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FlirtingApp.Web.Middleware
{
	public class AppExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public AppExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception e)
			{
				HandleGlobalException(httpContext, e);
			}
		}

		public void HandleGlobalException(HttpContext httpContext, Exception e)
		{
			var statusCode = HttpStatusCode.InternalServerError;

			switch (e)
			{
				case InvalidRefreshTokenException refreshToken:
				case InvalidJwtException invalidJwt:
					statusCode = HttpStatusCode.BadRequest;
					break;
				case ResourceNotFoundException resourceNotFound:
					statusCode = HttpStatusCode.NotFound;
					break;
			}

			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = (int)statusCode;

			var errorResult = JsonSerializer.Serialize(new
			{
				Errors = new
				{
					ClientErrorMessage = new List<string>
					{
						e.Message
					}
				}
			});

			httpContext.Response.WriteAsync(errorResult);
		}
	}
}
