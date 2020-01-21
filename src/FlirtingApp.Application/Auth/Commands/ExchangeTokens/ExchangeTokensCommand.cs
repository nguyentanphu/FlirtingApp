using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using MediatR;

namespace FlirtingApp.Application.Auth.Commands.ExchangeTokens
{
	public class ExchangeTokensCommand: BaseTokensModel, IRequest<ExchangeTokensCommandResult>
	{
		public string RemoteIpAddress { get; set; }
	}

	public class ExchangeTokensCommandResult : BaseTokensModel
	{

	}

	public class ExchangeTokensCommandHandler : IRequestHandler<ExchangeTokensCommand, ExchangeTokensCommandResult>
	{
		private readonly IJwtFactory _jwtFactory;
		private readonly ISecurityUserManager _userManager;

		public ExchangeTokensCommandHandler(IJwtFactory jwtFactory, ISecurityUserManager userManager)
		{
			_jwtFactory = jwtFactory;
			_userManager = userManager;
		}

		public async Task<ExchangeTokensCommandResult> Handle(ExchangeTokensCommand request, CancellationToken cancellationToken)
		{
			var claimPrincipal = _jwtFactory.GetClaimPrinciple(request.AccessToken);
			var securityUserId = Guid.Parse(claimPrincipal.FindFirst(AppClaimTypes.SecurityUserId).Value);
			var userId = Guid.Parse(claimPrincipal.FindFirst(AppClaimTypes.UserId).Value);
			var userName = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

			var newRefreshToken = await _userManager.ExchangeRefreshTokenAsync(securityUserId, request.RefreshToken, request.RemoteIpAddress);
			var newAccessToken = _jwtFactory.GenerateEncodedTokens(userId, securityUserId, userName);

			return new ExchangeTokensCommandResult
			{
				RefreshToken = newRefreshToken,
				AccessToken = newAccessToken
			};
		}
	}
}
