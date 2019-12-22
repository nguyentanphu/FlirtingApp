using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlirtingApp.Application.Auth.Commands.Login;
using FluentAssertions;
using Xunit;

namespace FlirtingApp.Application.Tests.Auth.Commands.Login
{
	public class LoginCommandValidatorTests
	{
		private readonly LogoutCommandValidator _sut = new LogoutCommandValidator();

		[Fact]
		public void AllEmpty_ValidatorContain3EmptyErrors()
		{
			var loginCommand = new LoginCommand();
			var result = _sut.Validate(loginCommand);

			result.Errors.Should().HaveCount(3);
		}

		[Fact]
		public void UserNameLessThan4_ValidatorHasError()
		{
			var loginCommand = new LoginCommand
			{
				UserName = "abc"
			};
			var result = _sut.Validate(loginCommand);

			result.Errors.Any(e => e.PropertyName == nameof(LoginCommand.UserName)).Should().BeTrue();
		}

		[Fact]
		public void UserNameMoreThan20_ValidatorHasError()
		{
			var loginCommand = new LoginCommand
			{
				UserName = "020019064676882329061"
			};
			var result = _sut.Validate(loginCommand);

			result.Errors.Any(e => e.PropertyName == nameof(LoginCommand.UserName)).Should().BeTrue();
		}

		[Fact]
		public void PasswordLessThan6_ValidatorHasError()
		{
			var loginCommand = new LoginCommand
			{
				UserName = "02001906467688232",
				Password = "123"
			};
			var result = _sut.Validate(loginCommand);

			result.Errors.Any(e => e.PropertyName == nameof(LoginCommand.Password)).Should().BeTrue();
		}

		[Fact]
		public void PasswordContainWhiteSpace_ValidatorHasError()
		{
			var loginCommand = new LoginCommand
			{
				UserName = "02001906467688232",
				Password = "123  456"
			};
			var result = _sut.Validate(loginCommand);

			result.Errors.Any(e => e.PropertyName == nameof(LoginCommand.Password)).Should().BeTrue();
		}
	}
}
