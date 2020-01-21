using FluentValidation;

namespace FlirtingApp.Application.Auth.Commands.ExchangeTokens
{
	class ExchangeTokensCommandValidator: AbstractValidator<ExchangeTokensCommand>
	{
		public ExchangeTokensCommandValidator()
		{
			RuleFor(e => e.AccessToken).NotEmpty().MinimumLength(100);
			RuleFor(e => e.RefreshToken).NotEmpty().MinimumLength(10);
			RuleFor(e => e.RemoteIpAddress).NotEmpty();
		}
	}
}
