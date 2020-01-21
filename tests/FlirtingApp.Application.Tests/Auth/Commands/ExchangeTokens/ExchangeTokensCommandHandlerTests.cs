using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.ExchangeTokens;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.Identity;
using FluentAssertions;
using Moq;
using Xunit;

namespace FlirtingApp.Application.Tests.Auth.Commands.ExchangeTokens
{
	public class ExchangeTokensCommandHandlerTests
	{
		private readonly ExchangeTokensCommandHandler _sut;
		private readonly Mock<IJwtFactory> _jwtFactory;
		private readonly Mock<ISecurityUserManager> _securityUserManager;

		public ExchangeTokensCommandHandlerTests()
		{
			_jwtFactory = new Mock<IJwtFactory>(MockBehavior.Strict);
			_securityUserManager = new Mock<ISecurityUserManager>(MockBehavior.Strict);

			_sut = new ExchangeTokensCommandHandler(_jwtFactory.Object, _securityUserManager.Object);
		}

		[Fact]
		public async Task Handle_JwtFactoryGetIncorrectSecurityUserId_GuidParseThrowException()
		{
			_jwtFactory.Setup(j => j
				.GetClaimPrinciple(It.IsAny<string>()))
				.Returns(new ClaimsPrincipal(
					new[]
					{
						new ClaimsIdentity(new[]
							{new Claim(AppClaimTypes.SecurityUserId, "abc")})
					}));
			Func<Task> act = async () => { await _sut.Handle(new ExchangeTokensCommand(), CancellationToken.None); };

			await act.Should().ThrowAsync<FormatException>();
		}

		[Fact]
		public async Task Handle_JwtFactoryGetIncorrectAppUserId_GuidParseThrowException()
		{
			_jwtFactory.Setup(j => j
					.GetClaimPrinciple(It.IsAny<string>()))
				.Returns(new ClaimsPrincipal(
					new[]
					{
						new ClaimsIdentity(new[]
							{
								new Claim(AppClaimTypes.SecurityUserId, Guid.NewGuid().ToString()),
								new Claim(AppClaimTypes.UserId, "xyz"),
							}
						)
					}));
			Func<Task> act = async () => { await _sut.Handle(new ExchangeTokensCommand(), CancellationToken.None); };

			await act.Should().ThrowAsync<FormatException>();
		}

		[Fact]
		public async Task Handle_Success()
		{
			var refreshToken = "12345678910";
			var accessToken = "nX8iHi4QnJP8aFzf4o7vtWUMEnxN2ZUDJnM0ogNjuMNKb9kt8pn6KLaeCBVP";
			var userName = "phunguyen";
			var remoteIpAddress = "192.168.1.1";
			var newRefreshToken = "new refresh token";
			var newJwtToken = "new jwt token";
			_jwtFactory.Setup(j => j
					.GetClaimPrinciple(It.IsAny<string>()))
				.Returns(new ClaimsPrincipal(
					new[]
					{
						new ClaimsIdentity(new[]
							{
								new Claim(AppClaimTypes.SecurityUserId, Guid.NewGuid().ToString()),
								new Claim(AppClaimTypes.UserId, Guid.NewGuid().ToString()),
								new Claim(ClaimTypes.NameIdentifier, userName), 
							}
						)
					}));
			_jwtFactory.Setup(j => j.GenerateEncodedTokens(It.IsAny<Guid>(), It.IsAny<Guid>(), userName))
				.Returns(newJwtToken);

			_securityUserManager.Setup(s => s.ExchangeRefreshTokenAsync(It.IsAny<Guid>(), refreshToken, remoteIpAddress))
				.Returns(Task.FromResult(newRefreshToken));


			var result = await _sut.Handle(new ExchangeTokensCommand
			{
				RefreshToken = refreshToken,
				AccessToken = accessToken,
				RemoteIpAddress = remoteIpAddress
			}, CancellationToken.None);

			result.RefreshToken.Should().Be(newRefreshToken);
			result.AccessToken.Should().Be(newJwtToken);
		}

	}
}
