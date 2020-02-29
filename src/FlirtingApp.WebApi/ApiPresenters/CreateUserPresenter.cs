using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.ApiPresenters
{
	public sealed class CreateUserPresenter: ApiPresenterBase<Result<Guid>>
	{
		public override void Handle(Result<Guid> model)
		{
			if (model.Success)
			{
				Result = new CreatedAtRouteResult("GetUser", new {id = model.Value}, null);
			}
			else
			{
				var errorObject = new
				{
					Errors = new
					{
						ErrorMessage = new List<string>
						{
							model.Error
						}
					}
				};
				Result = new BadRequestObjectResult(errorObject);
			}
		}
	}
}
