using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.Extensions
{
	public static class ActionResultExtensions
	{
		public static IActionResult ToBadRequestResult(this Result result)
		{
			var errorObject = new
			{
				Errors = new
				{
					ErrorMessage = new List<string>
					{
						result.Error
					}
				}
			};
			return new BadRequestObjectResult(errorObject);
		}
	}
}
