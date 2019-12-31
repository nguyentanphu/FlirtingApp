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
			RuleFor(u => u.UserName).NotEmpty().MinimumLength(6).MaximumLength(20);
			RuleFor(u => u.Password).NotEmpty().MinimumLength(6);
			RuleFor(u => u.FirstName).NotEmpty().MinimumLength(3);
			RuleFor(u => u.LastName).NotEmpty().MinimumLength(3);
			RuleFor(u => u.Email).NotEmpty().EmailAddress();
			RuleFor(u => u.DateOfBirth).NotEmpty().LessThanOrEqualTo(new DateTime(2000, 1, 1));
			RuleFor(u => u.Coordinates).Must(c => c.Length == 2).Unless(c => c == null);
		}
	}
}
