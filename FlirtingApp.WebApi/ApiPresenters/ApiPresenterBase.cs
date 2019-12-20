using System.Collections.Generic;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.ApiPresenters
{
	public abstract class ApiPresenterBase<TResponse> : IOutputPort<TResponse> where TResponse : ResponseBase
	{
		public IActionResult Result { get; set; }

		public virtual void Handle(TResponse model)
		{
			if (model.Success)
			{
				Result = new OkObjectResult(model);
			}
			else
			{
				var errorObject = new
				{
					Errors = new
					{
						ErrorMessage = new List<string>
						{
							model.ErrorMessage
						}
					}
				};
				Result = new BadRequestObjectResult(errorObject);
			}
		}
	}
}
