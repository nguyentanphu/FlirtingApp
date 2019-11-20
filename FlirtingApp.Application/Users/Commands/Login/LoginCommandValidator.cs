using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlirtingApp.Application.Users.Commands.Login
{
	public class LoginCommandValidator: AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(l => l.UserName).MinimumLength(6).MaximumLength(20).NotEmpty();
			RuleFor(l => l.Password).MinimumLength(6).NotEmpty();
			RuleFor(l => l.RemoteIpAddress).NotEmpty();
		}
	}
}
