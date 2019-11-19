using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Application.Common.Responses;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Users.Commands.Login
{
	public class LoginCommand: IRequest<BaseTokensResponse>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string RemoteIpAddress { get; set; }
	}

	public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseTokensResponse>
	{
		private readonly IAppUserManager _userManager;
		private readonly ITokenFactory _tokenFactory;
		private readonly IJwtFactory _jwtFactory;
		private readonly IAppDbContext _dbContext;

		public LoginCommandHandler(IAppUserManager userManager, ITokenFactory tokenFactory, IJwtFactory jwtFactory, IAppDbContext dbContext)
		{
			_userManager = userManager;
			_tokenFactory = tokenFactory;
			_jwtFactory = jwtFactory;
			_dbContext = dbContext;
		}

		public async Task<BaseTokensResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var loginResult = await _userManager.LoginUserAsync(
				request.UserName, 
				request.Password, 
				request.RemoteIpAddress
			);
			if (!loginResult.Success)
			{
				throw new LoginException();
			}

			var user = await GetUserByIdentityUser(loginResult.AppUserId);
			var accessToken = _jwtFactory.GenerateEncodedTokens(user.UserId, loginResult.AppUserId, user.UserName);

			return new BaseTokensResponse
			{
				AccessToken = accessToken,
				RefreshToken = loginResult.RefreshToken
			};
		}

		private async Task<User> GetUserByIdentityUser(Guid appUserId)
		{
			return await _dbContext.Users.FirstAsync(u => u.IdentityId == appUserId);
		}
	}
}
