using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using MediatR;

namespace FlirtingApp.Application.Users.Commands.ExchangeTokens
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
		private readonly IAppUserManager _userManager;

		public ExchangeTokensCommandHandler(IJwtFactory jwtFactory, IAppUserManager userManager)
		{
			_jwtFactory = jwtFactory;
			_userManager = userManager;
		}

		public async Task<ExchangeTokensCommandResult> Handle(ExchangeTokensCommand request, CancellationToken cancellationToken)
		{
			var claimPrincipal = _jwtFactory.GetClaimPrinciple(request.AccessToken);
			var appUserId = Guid.Parse(claimPrincipal.FindFirst(AppClaimTypes.AppUserId).Value);
			var userId = Guid.Parse(claimPrincipal.FindFirst(AppClaimTypes.UserId).Value);
			var userName = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

			var newRefreshToken = await _userManager.ExchangeRefreshTokenAsync(appUserId, request.RefreshToken, request.RemoteIpAddress);
			var newAccessToken = _jwtFactory.GenerateEncodedTokens(userId, appUserId, userName);

			return new ExchangeTokensCommandResult
			{
				RefreshToken = newRefreshToken,
				AccessToken = newAccessToken
			};
		}
	}
}
