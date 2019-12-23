using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Infrastructure.Exceptions;
using FlirtingApp.Infrastructure.Identity;
using FlirtingApp.Infrastructure.Identity.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace FlirtingApp.Infrastructure.Tests.Identity
{
	public class SecurityUserManagerTests: IClassFixture<IdentityDbContextFixture>
	{
		private readonly Mock<UserManager<SecurityUser>> _userManager;
		private readonly IdentityDbContext _context;
		private readonly Mock<ITokenFactory> _tokenFactory;
		private readonly SecurityUserManager _sut;

		public SecurityUserManagerTests(IdentityDbContextFixture fixture)
		{
			var storeMock = new Mock<IUserStore<SecurityUser>>();
			_userManager = new Mock<UserManager<SecurityUser>>(storeMock.Object, null, null, null, null, null, null, null, null);
			_tokenFactory = new Mock<ITokenFactory>(MockBehavior.Strict);
			_context = fixture.Context;

			_sut = new SecurityUserManager(_userManager.Object, _context, _tokenFactory.Object);
		}

		[Fact]
		public async Task UserNameExistAsync_True()
		{
			var result = await _sut.UserNameExistAsync("phunguyen");
			result.Should().BeTrue();
		}

		[Fact]
		public async Task UserNameExistAsync_False()
		{
			var result = await _sut.UserNameExistAsync("JustinBieber");
			result.Should().BeFalse();
		}

		[Fact]
		public void CreateUserAsync_ThrowCreateAppUserException()
		{
			var userName = "tina tinh";
			var password = "123456";

			_userManager.Setup(m => m.CreateAsync(It.Is<SecurityUser>(u => u.UserName == userName), password))
				.Returns(Task.FromResult(IdentityResult.Failed()));

			Func<Task<Guid>> act = async () => await _sut.CreateUserAsync(userName, password);

			act.Should().Throw<CreateAppUserException>();
		}

		[Fact]
		public async Task CreateUserAsync_Success()
		{
			var userName = "tina tinh";
			var password = "123456";
			var id = Guid.NewGuid();

			_userManager.Setup(m => m.CreateAsync(It.Is<SecurityUser>(u => u.UserName == userName), password))
				.Callback((SecurityUser u, string p) => { u.Id = id; })
				.Returns(Task.FromResult(IdentityResult.Success));

			var result = await _sut.CreateUserAsync(userName, password);

			result.Should().Be(id);
		}

		[Fact]
		public async Task LoginUserAsync_NotFoundUser()
		{
			var userName = "tina tinh";
			var password = "123456";
			var remoteIp = "192.168.1.1";

			_userManager.Setup(m => m.FindByNameAsync(userName))
				.Returns(Task.FromResult<SecurityUser>(default));

			var result = await _sut.LoginUserAsync(userName, password, remoteIp);

			result.Success.Should().BeFalse();
		}

		[Fact]
		public async Task LoginUserAsync_NotMatchedPassword()
		{
			var userName = "tina tinh";
			var password = "123456";
			var remoteIp = "192.168.1.1";
			var user = new SecurityUser
			{
				UserName = userName,
			};

			_userManager.Setup(m => m.FindByNameAsync(userName))
				.Returns(Task.FromResult(user));

			_userManager.Setup(m => m.CheckPasswordAsync(user, password))
				.Returns(Task.FromResult(false));

			var result = await _sut.LoginUserAsync(userName, password, remoteIp);

			result.Success.Should().BeFalse();
		}

		[Fact]
		public async Task LoginUserAsync_Success()
		{
			var userName = TestIdentityDbContextFactory.DefaultUserName;
			var password = "123456";
			var remoteIp = TestIdentityDbContextFactory.DefaultIp;
			var refreshToken = "frtrykvddfnme";

			_userManager.Setup(m => m.FindByNameAsync(userName))
				.Returns((string userName) => Task.FromResult(_context.AppUsers.First(u => u.UserName == userName)));

			_userManager.Setup(m => m.CheckPasswordAsync(It.Is<SecurityUser>(u => u.UserName == userName), password))
				.Returns(Task.FromResult(true));

			_tokenFactory.Setup(t => t.GenerateToken(It.IsAny<int>()))
				.Returns(refreshToken);

			var result = await _sut.LoginUserAsync(userName, password, remoteIp);

			result.Success.Should().BeTrue();
			result.RefreshToken.Should().Be(refreshToken);
			result.SecurityUserId.Should().Be(TestIdentityDbContextFactory.DefaultId);

			_context.RefreshTokens.Any(r => r.Token == refreshToken).Should().BeTrue();
		}

		[Fact]
		public async Task LogoutUserAsync_Success()
		{
			var id = TestIdentityDbContextFactory.DefaultId;
			var remoteIp = TestIdentityDbContextFactory.DefaultIp;
			var refreshToken = TestIdentityDbContextFactory.DefaultRefreshToken;

			await _sut.LogoutUserAsync(id, remoteIp);
			_context.RefreshTokens.Any(r => r.Token == refreshToken).Should().BeFalse();
		}


	}
}
