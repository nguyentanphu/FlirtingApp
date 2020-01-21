using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.Logout;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using Moq;
using Xunit;

namespace FlirtingApp.Application.Tests.Auth.Commands.Logout
{
	public class LogoutCommandHandlerTests
	{
		private readonly LogoutCommandHandler _sut;
		private readonly Mock<ICurrentUser> _currentUser;
		private readonly Mock<ISecurityUserManager> _securityUserManager;

		public LogoutCommandHandlerTests()
		{
			_currentUser = new Mock<ICurrentUser>(MockBehavior.Strict);
			_securityUserManager = new Mock<ISecurityUserManager>(MockBehavior.Strict);

			_sut = new LogoutCommandHandler(_currentUser.Object, _securityUserManager.Object);
		}

		[Fact]
		public async Task LogoutSuccess()
		{
			var remoteIpAddress = "192.168.1.1";
			var securityUserId = Guid.NewGuid();
			_currentUser.SetupGet(c => c.SecurityUserId).Returns(securityUserId);
			_securityUserManager.Setup(m => m.LogoutUserAsync(securityUserId, remoteIpAddress))
				.Returns(Task.CompletedTask);

			await _sut.Handle(new LogoutCommand
			{
				RemoteIpAddress = remoteIpAddress
			}, CancellationToken.None);

			_currentUser.Verify(c => c.SecurityUserId, Times.Once);
			_securityUserManager.Verify(m => m.LogoutUserAsync(securityUserId, remoteIpAddress), Times.Once);
		}
	}
}
