using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Auth.Commands.Login
{
	public class LoginCommand: LoginRequest, IRequest<LoginCommandResponse>
	{
		public string RemoteIpAddress { get; set; }
	}

	public class LoginCommandResponse : BaseTokensModel
	{

	}

	public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
	{
		private readonly IAppUserManager _userManager;
		private readonly IJwtFactory _jwtFactory;
		private readonly IAppDbContext _dbContext;

		public LoginCommandHandler(IAppUserManager userManager, IJwtFactory jwtFactory, IAppDbContext dbContext)
		{
			_userManager = userManager;
			_jwtFactory = jwtFactory;
			_dbContext = dbContext;
		}

		public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
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

			return new LoginCommandResponse
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
