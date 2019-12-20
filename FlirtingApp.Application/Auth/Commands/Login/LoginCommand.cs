using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Domain.Entities;
using MediatR;

namespace FlirtingApp.Application.Auth.Commands.Login
{
	public class LoginCommand: RequestBase<LoginCommandResponse>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string RemoteIpAddress { get; set; }
	}

	public class LoginCommandResponse : ResponseBase
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}

	public class LoginCommandHandler : IRequestHandler<LoginCommand>
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

		public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var loginResult = await _userManager.LoginUserAsync(
				request.UserName, 
				request.Password, 
				request.RemoteIpAddress
			);
			if (!loginResult.Success)
			{
				request.OutputPort.Handle(new LoginCommandResponse
				{
					Success = false,
					ErrorMessage = "Login failed. Either user name or password is not correct"
				});
				return Unit.Value;
			}

			var user = await GetUserByIdentityUser(loginResult.SecurityUserId);
			var accessToken = _jwtFactory.GenerateEncodedTokens(user.Id, loginResult.SecurityUserId, user.UserName);

			request.OutputPort.Handle(new LoginCommandResponse
			{
				Success = true,
				AccessToken = accessToken,
				RefreshToken = loginResult.RefreshToken
			});
			return Unit.Value;
		}

		private async Task<User> GetUserByIdentityUser(Guid securityUserId)
		{
			return await _userRepository.GetAsync(u => u.IdentityId == securityUserId);
		}
	}
}
