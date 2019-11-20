using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlirtingApp.Application.Users.Commands.CreateUser
{
	public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(u => u.UserName).MinimumLength(6).MaximumLength(20);
			RuleFor(u => u.Password).MinimumLength(6);
			RuleFor(u => u.FirstName).MinimumLength(3);
			RuleFor(u => u.LastName).MinimumLength(3);
			RuleFor(u => u.Email).EmailAddress();
			RuleFor(u => u.DateOfBirth).LessThanOrEqualTo(new DateTime(2000, 1, 1));
		}
	}
}
