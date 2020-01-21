using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlirtingApp.Application.Users.Commands.UpdateUser
{
	public class UpdateUserAdditionalInfoCommandValidator: AbstractValidator<UpdateUserAdditionalInfoCommand>
	{
		public UpdateUserAdditionalInfoCommandValidator()
		{
			RuleFor(u => u.UserId).NotEmpty();
			RuleFor(u => u.KnownAs).MinimumLength(6).MaximumLength(20);
			RuleFor(u => u.Introduction).MinimumLength(20);
			RuleFor(u => u.Interests).MinimumLength(20);
			RuleFor(u => u.LookingFor).MinimumLength(20);
			RuleFor(u => u.City).MinimumLength(3);
			RuleFor(u => u.Country).MinimumLength(5);
		}
	}
}
