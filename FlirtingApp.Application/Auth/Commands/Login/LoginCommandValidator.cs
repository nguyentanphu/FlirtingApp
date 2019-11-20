using FluentValidation;

namespace FlirtingApp.Application.Auth.Commands.Login
{
	public class LoginCommandValidator: AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(l => l.UserName).MinimumLength(6).MaximumLength(20);
			RuleFor(l => l.Password).MinimumLength(6);
			RuleFor(l => l.RemoteIpAddress).NotEmpty();
		}
	}
}
