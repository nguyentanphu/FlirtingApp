using System.Collections.Generic;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.ApiPresenters
{
	public abstract class ApiPresenterBase<TResponse> : IOutputPort<TResponse> where TResponse : Result
	{
		public IActionResult Result { get; set; } = default!;

		public virtual void Handle(TResponse result)
		{
			if (result.Success)
			{
				Result = new OkObjectResult(result);
			}
			else
			{
				Result = result.ToBadRequestResult();
			}
		}
    }
}
