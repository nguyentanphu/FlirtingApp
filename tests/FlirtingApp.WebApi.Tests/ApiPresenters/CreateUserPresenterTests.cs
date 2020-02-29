using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Users.Commands.CreateUser;
using FlirtingApp.WebApi.ApiPresenters;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FlirtingApp.WebApi.Tests.ApiPresenters
{
	public class CreateUserPresenterTests
	{
		private readonly CreateUserPresenter _sut = new CreateUserPresenter();

		[Fact]
		public void Handle_Fail_ShouldBeBadRequestObjectResult()
		{
			_sut.Handle(Result.Fail<Guid>("Fail with unknown reason"));

			_sut.Result.Should().BeOfType<BadRequestObjectResult>();
		}

		[Fact]
		public void Handle_Success_ShouldBeBadRequestObjectResult()
		{
			_sut.Handle(Result.Ok(Guid.NewGuid()));

			_sut.Result.Should().BeOfType<CreatedAtRouteResult>();
		}
	}
}
