using FluentValidation;

namespace FlirtingApp.Application.Auth.Commands.ExchangeTokens
{
	public class ExchangeTokensCommandValidator: AbstractValidator<ExchangeTokensCommand>
	{
		public ExchangeTokensCommandValidator()
		{
			RuleFor(e => e.AccessToken).MinimumLength(100);
			RuleFor(e => e.RefreshToken).MinimumLength(10);
			RuleFor(e => e.RemoteIpAddress).NotEmpty();
		}
	}
}
