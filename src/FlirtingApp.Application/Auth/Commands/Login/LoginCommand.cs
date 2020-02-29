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
	public class LoginCommand: RequestBase<Result<BaseTokensModel>>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string RemoteIpAddress { get; set; }
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
			if (loginResult.Failure)
			{
				request.OutputPort.Handle(
					Result.Fail<BaseTokensModel>("Login failed. Either user name or password is not correct")
				);
				return Unit.Value;
			}

			var user = await GetUserByIdentityUser(loginResult.Value.SecurityUserId);
			var accessToken = _jwtFactory.GenerateEncodedTokens(user.Id, loginResult.Value.SecurityUserId, user.UserName);

			request.OutputPort.Handle(Result.Ok(new BaseTokensModel
			{
				AccessToken = accessToken,
				RefreshToken = loginResult.Value.RefreshToken
			}));

			return Unit.Value;
		}

		private async Task<User> GetUserByIdentityUser(Guid securityUserId)
		{
			return await _userRepository.GetAsync(u => u.IdentityId == securityUserId);
		}
	}
}
