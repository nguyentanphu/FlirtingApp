using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.Login;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FlirtingApp.Domain.Common;
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
		private readonly Mock<IOutputPort<Result<BaseTokensModel>>> _outputPort;

		public LoginCommandHandlerTests()
		{
			_securityUserManager = new Mock<ISecurityUserManager>(MockBehavior.Strict);
			_jwtFactory = new Mock<IJwtFactory>(MockBehavior.Strict);
			_userRepo = new Mock<IUserRepository>(MockBehavior.Strict);

			_sut = new LoginCommandHandler(_securityUserManager.Object, _jwtFactory.Object, _userRepo.Object);

			_outputPort = new Mock<IOutputPort<Result<BaseTokensModel>>>(MockBehavior.Strict);
			
		}

		[Fact]
		public async Task Handle_LoginFailed_InvalidUserNameOrPassword()
		{
			_securityUserManager.Setup(u => u.LoginUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult<Result<(Guid SecurityUserId, string RefreshToken)>>(Result.Fail<(Guid SecurityUserId, string RefreshToken)>("Login failed")));
			_outputPort.Setup(o => o.Handle(It.Is<Result<BaseTokensModel>>(
				r => 
					!r.Success && 
					r.Error == "Login failed. Either user name or password is not correct"
			)));

			await _sut.Handle(new LoginCommand
			{
				UserName = "abc",
				Password = "123456",
				RemoteIpAddress = "192.168.1.1",
				OutputPort = _outputPort.Object
			}, CancellationToken.None);

			_outputPort.Verify(o => o.Handle(It.IsAny<Result<BaseTokensModel>>()), Times.Once);
		}

		[Fact]
		public async Task Handle_LoginSucceed_ReturnTokens()
		{
			var securityUserId = Guid.NewGuid();
			var userName = "phunguyen";
			var refreshToken = "refresh token";
			var accessToken = "access token";
			
			_securityUserManager.Setup(u => u.LoginUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult<Result<(Guid SecurityUserId, string RefreshToken)>>(Result.Ok((securityUserId, refreshToken))));

			_userRepo.Setup(u => u.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(new User(
					Guid.NewGuid(),
					userName,
					"phu",
					"nguyen",
					"nguyentanphu@hotmail.com",
					new DateTime(1992, 5, 18),
					Gender.Male,
					DateTime.UtcNow
				)));

			_jwtFactory.Setup(j => j.GenerateEncodedTokens(It.IsAny<Guid>(), securityUserId, userName))
				.Returns(accessToken);

			_outputPort.Setup(o => o.Handle(It.Is<Result<BaseTokensModel>>(
				r =>
					r.Success &&
					r.Value.RefreshToken == refreshToken &&
					r.Value.AccessToken == accessToken
			)));

			await _sut.Handle(new LoginCommand
			{
				UserName = "phunguyen",
				Password = "123456789",
				RemoteIpAddress = "192.168.1.1",
				OutputPort = _outputPort.Object
			}, CancellationToken.None);

			_outputPort.Verify(o => o.Handle(It.IsAny<Result<BaseTokensModel>>()), Times.Once);
		}
	}
}
