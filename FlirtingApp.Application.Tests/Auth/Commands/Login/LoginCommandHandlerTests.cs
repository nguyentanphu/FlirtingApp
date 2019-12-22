using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.Login;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Domain.Entities;
using Moq;
using Xunit;

namespace FlirtingApp.Application.Tests.Auth.Commands.Login
{
	public class LoginCommandHandlerTests
	{
		private readonly LoginCommandHandler _sut;
		private readonly Mock<ISecurityUserManager> _securityUserManager;
		private readonly Mock<IJwtFactory> _jwtFactory;
		private readonly Mock<IUserRepository> _userRepo;
		private readonly Mock<IOutputPort<LoginCommandResponse>> _outputPort;

		public LoginCommandHandlerTests()
		{
			_securityUserManager = new Mock<ISecurityUserManager>(MockBehavior.Strict);
			_jwtFactory = new Mock<IJwtFactory>(MockBehavior.Strict);
			_userRepo = new Mock<IUserRepository>(MockBehavior.Strict);

			_sut = new LoginCommandHandler(_securityUserManager.Object, _jwtFactory.Object, _userRepo.Object);

			_outputPort = new Mock<IOutputPort<LoginCommandResponse>>(MockBehavior.Strict);
			
		}

		[Fact]
		public async Task Handle_LoginFailed_InvalidUserNameOrPassword()
		{
			_securityUserManager.Setup(u => u.LoginUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult<(bool Success, Guid SecurityUserId, string RefreshToken)>((false, default, default)));
			_outputPort.Setup(o => o.Handle(It.Is<LoginCommandResponse>(
				r => 
					!r.Success && 
					r.ErrorMessage == "Login failed. Either user name or password is not correct"
			)));

			await _sut.Handle(new LoginCommand
			{
				UserName = "abc",
				Password = "123456",
				RemoteIpAddress = "192.168.1.1",
				OutputPort = _outputPort.Object
			}, CancellationToken.None);

			_outputPort.Verify(o => o.Handle(It.IsAny<LoginCommandResponse>()), Times.Once);
		}

		[Fact]
		public async Task Handle_LoginSucceed_ReturnTokens()
		{
			var securityUserId = Guid.NewGuid();
			var userId = Guid.NewGuid();
			var userName = "phunguyen";
			var refreshToken = "refresh token";
			var accessToken = "access token";
			
			_securityUserManager.Setup(u => u.LoginUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult<(bool Success, Guid SecurityUserId, string RefreshToken)>((true, securityUserId, refreshToken)));

			_userRepo.Setup(u => u.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(new User {Id = userId, UserName = userName}));

			_jwtFactory.Setup(j => j.GenerateEncodedTokens(userId, securityUserId, userName))
				.Returns(accessToken);

			_outputPort.Setup(o => o.Handle(It.Is<LoginCommandResponse>(
				r =>
					r.Success &&
					r.RefreshToken == refreshToken &&
					r.AccessToken == accessToken
			)));

			await _sut.Handle(new LoginCommand
			{
				UserName = "phunguyen",
				Password = "123456789",
				RemoteIpAddress = "192.168.1.1",
				OutputPort = _outputPort.Object
			}, CancellationToken.None);

			_outputPort.Verify(o => o.Handle(It.IsAny<LoginCommandResponse>()), Times.Once);
		}
	}
}
