using FluentValidation;

namespace FlirtingApp.Application.Auth.Commands.Login
{
	public class LoginCommandValidator: AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(l => l.UserName).MinimumLength(4).MaximumLength(20);
			RuleFor(l => l.Password)
				.MinimumLength(6)
				// Non space chars only
				.Matches("^\\S*$").WithMessage("Password cannot contain white spaces");
			RuleFor(l => l.RemoteIpAddress).NotEmpty();
		}
	}
}
