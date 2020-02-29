using FlirtingApp.Application.Auth.Commands.Login;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Users.Commands.CreateUser;
using FlirtingApp.WebApi.ApiPresenters;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FlirtingApp.WebApi.Tests.ApiPresenters
{
	public class LoginPresenterTests
	{
		private readonly LoginPresenter _sut = new LoginPresenter();

		[Fact]
		public void Handle_Fail_ShouldBeBadRequestObjectResult()
		{
			_sut.Handle(Result.Fail<BaseTokensModel>("Fail with unknown reason"));

			_sut.Result.Should().BeOfType<BadRequestObjectResult>();
		}

		[Fact]
		public void Handle_Success_ShouldBeBadRequestObjectResult()
		{
			_sut.Handle(Result.Ok(new BaseTokensModel()));

			_sut.Result.Should().BeOfType<OkObjectResult>();
		}
	}
}
