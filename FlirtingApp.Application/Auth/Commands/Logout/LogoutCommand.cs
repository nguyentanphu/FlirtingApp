using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using MediatR;

namespace FlirtingApp.Application.Auth.Commands.Logout
{
	public class LogoutCommand: IRequest
	{
		public string RemoteIpAddress { get; set; }
	}

	public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
	{
		private readonly ISecurityUserManager _userManager;
		private readonly ICurrentUser _currentUser;

		public LogoutCommandHandler(ICurrentUser currentUser, ISecurityUserManager userManager)
		{
			_currentUser = currentUser;
			_userManager = userManager;
		}

		public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
		{
			await _userManager.LogoutUserAsync(_currentUser.SecurityUserId.Value, request.RemoteIpAddress);
			return Unit.Value;
		}
	}

}
