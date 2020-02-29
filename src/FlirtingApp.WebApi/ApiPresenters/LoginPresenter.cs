using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.Login;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.ApiPresenters
{
	public sealed class LoginPresenter: ApiPresenterBase<Result<BaseTokensModel>>
	{
		public override void Handle(Result<BaseTokensModel> result)
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
