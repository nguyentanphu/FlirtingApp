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
		private readonly ISecurityUserManager _userManager;
		private readonly IJwtFactory _jwtFactory;
		private readonly IUserRepository _userRepository;

		public LoginCommandHandler(ISecurityUserManager userManager, IJwtFactory jwtFactory, IUserRepository userRepository)
		{
			_userManager = userManager;
			_jwtFactory = jwtFactory;
			_userRepository = userRepository;
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

			var user = await GetUserByIdentityUser(loginResult.SecurityUserId);
			var accessToken = _jwtFactory.GenerateEncodedTokens(user.Id, loginResult.SecurityUserId, user.UserName);

			return new LoginCommandResponse
			{
				AccessToken = accessToken,
				RefreshToken = loginResult.RefreshToken
			};
		}

		private async Task<User> GetUserByIdentityUser(Guid securityUserId)
		{
			return await _userRepository.GetAsync(u => u.IdentityId == securityUserId);
		}
	}
}
