using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using MediatR;
using NotImplementedException = System.NotImplementedException;

namespace FlirtingApp.Application.Users.Commands.Login
{
	public class LoginCommand: IRequest<LoginCommandReponse>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string RemoteIpAddress { get; set; }
	}

	public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandReponse>
	{
		private readonly IAppUserManager _userManager;
		private readonly ITokenFactory _tokenFactory;

		public async Task<LoginCommandReponse> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var newRefreshToken = _tokenFactory.GenerateToken();
			var loginResult = await _userManager.LoginUserAsync(request.UserName, request.Password, newRefreshToken, request.RemoteIpAddress);

		}
	}
}
