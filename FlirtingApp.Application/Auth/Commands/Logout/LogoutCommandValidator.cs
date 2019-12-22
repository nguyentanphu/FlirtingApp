using FlirtingApp.Application.Auth.Commands.Login;
using FluentValidation;

namespace FlirtingApp.Application.Auth.Commands.Logout
{
	class LogoutCommandValidator: AbstractValidator<LogoutCommand>
	{
		public LogoutCommandValidator()
		{
			RuleFor(l => l.RemoteIpAddress).NotEmpty();
		}
	}
}
