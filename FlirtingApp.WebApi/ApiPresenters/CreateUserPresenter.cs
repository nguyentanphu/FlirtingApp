using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.ApiPresenters
{
	public sealed class CreateUserPresenter: ApiPresenterBase<CreateUserCommandResponse>
	{
		public override void Handle(CreateUserCommandResponse model)
		{
			if (model.Success)
			{
				Result = new NoContentResult();
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
