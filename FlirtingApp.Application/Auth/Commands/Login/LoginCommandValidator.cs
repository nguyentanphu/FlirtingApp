using FluentValidation;

namespace FlirtingApp.Application.Auth.Commands.Login
{
	class LogoutCommandValidator: AbstractValidator<LoginCommand>
	{
		public LogoutCommandValidator()
		{
			RuleFor(l => l.UserName)
				.NotEmpty()
				.MinimumLength(4)
				.MaximumLength(20);

			RuleFor(l => l.Password)
				.NotEmpty()
				.MinimumLength(6)
				// Non space chars only
				.Matches("^\\S*$").WithMessage("Password cannot contain white spaces");
			RuleFor(l => l.RemoteIpAddress).NotEmpty();
		}
	}
}
