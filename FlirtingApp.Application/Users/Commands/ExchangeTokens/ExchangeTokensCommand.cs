using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Application.Common.Responses;
using MediatR;

namespace FlirtingApp.Application.Users.Commands.ExchangeTokens
{
	public class ExchangeTokensCommand: IRequest<BaseTokensResponse>
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public string RemoteIpAddress { get; set; }
	}

	public class ExchangeTokensCommandHandler : IRequestHandler<ExchangeTokensCommand, BaseTokensResponse>
	{
		private readonly IJwtFactory _jwtFactory;
		private readonly IAppUserManager _userManager;

		public ExchangeTokensCommandHandler(IJwtFactory jwtFactory)
		{
			_jwtFactory = jwtFactory;
		}

		public async Task<BaseTokensResponse> Handle(ExchangeTokensCommand request, CancellationToken cancellationToken)
		{
			var claimPrincipal = _jwtFactory.GetClaimPrinciple(request.AccessToken);
			var appUserId = Guid.Parse(claimPrincipal.FindFirst(c => c.Type == AppClaimTypes.AppUserId).Value);
			var userId = Guid.Parse(claimPrincipal.FindFirst(c => c.Type == AppClaimTypes.UserId).Value);
			var userName = claimPrincipal.FindFirst(c => c.Type == AppClaimTypes.Sub).Value;

			var newRefreshToken = await _userManager.ExchangeRefreshTokenAsync(appUserId, request.RefreshToken, request.RemoteIpAddress);
			var newAccessToken = _jwtFactory.GenerateEncodedTokens(userId, appUserId, userName);

			return new BaseTokensResponse
			{
				RefreshToken = newRefreshToken,
				AccessToken = newAccessToken
			};
		}
	}
}
