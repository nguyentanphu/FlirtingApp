using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FlirtingApp.Application;
using FlirtingApp.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FlirtingApp.WebApi.Middleware
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
			var jsonSettings = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};
			var errorResult = GenerateErrorsJsonFromErrorMessage(e.Message, jsonSettings);

			switch (e)
			{
				case AppValidationException validationException:
					statusCode = HttpStatusCode.BadRequest;
					errorResult = GenerateErrorsJsonFromValidationErrors(validationException.Failures, jsonSettings);
					break;
				case InvalidRefreshTokenException _:
				case InvalidJwtException _:
				case LoginException _:
					statusCode = HttpStatusCode.BadRequest;
					break;
				case ResourceNotFoundException _:
					statusCode = HttpStatusCode.NotFound;
					break;
			}

			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = (int)statusCode;


			httpContext.Response.WriteAsync(errorResult);
		}

		private string GenerateErrorsJsonFromErrorMessage(string message, JsonSerializerOptions jsonSerializerOptions)
		{
			return JsonSerializer.Serialize(new
			{
				Errors = new
				{
					ErrorMessage = new List<string>
					{
						message
					}
				}
			}, jsonSerializerOptions);
		}

		private string GenerateErrorsJsonFromValidationErrors(IDictionary<string, string[]> errors, JsonSerializerOptions jsonSerializerOptions)
		{
			return JsonSerializer.Serialize(new
			{
				Errors = errors
			}, jsonSerializerOptions);
		} 
	}
}
