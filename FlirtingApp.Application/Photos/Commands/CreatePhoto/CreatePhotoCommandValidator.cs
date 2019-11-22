using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlirtingApp.Application.Photos.Commands.CreatePhoto
{
	public class CreatePhotoCommandValidator: AbstractValidator<CreatePhotoCommand>
	{
		public CreatePhotoCommandValidator()
		{
			RuleFor(p => p.UserId).NotEmpty();
			RuleFor(p => p.Description).MinimumLength(10).MaximumLength(1000);
			RuleFor(p => p.File).NotNull();
			RuleFor(p => p.File.Length).NotEmpty();
		}
	}
}
