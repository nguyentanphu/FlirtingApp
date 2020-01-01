using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Persistent.Mongo;
using FlirtingApp.Persistent.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FlirtingApp.Persistent.Tests
{
	public class UserRepositoryTests: IClassFixture<AppDbContextFixture>
	{
		private readonly UserRepository _sut;
		private readonly AppDbContext _context;
		private readonly Mock<IMongoRepository<User>> _mongoRepository;
		public UserRepositoryTests(AppDbContextFixture fixture)
		{
			_context = fixture.Context;
			_mongoRepository = new Mock<IMongoRepository<User>>(MockBehavior.Strict);

			_sut = new UserRepository(_context, _mongoRepository.Object);
		}

		[Fact]
		public async Task GetAsync_WithId_Success()
		{
			var id = Guid.NewGuid();
			var user = new User(
				Guid.NewGuid(),
				"phunguyenId",
				"phu",
				"nguyen",
				"nguyentanphu@hotmail.com",
				new DateTime(1992, 5, 18),
				Gender.Male,
				DateTime.UtcNow
			);
			_mongoRepository.Setup(m => m.GetAsync(It.IsAny<Guid>()))
				.Returns(Task.FromResult(user));

			var result = await _sut.GetAsync(id);

			result.Should().Be(user);
		}
		[Fact]
		public async Task GetAsync_WithPredicate_Success()
		{
			var user = new User(
				Guid.NewGuid(),
				"withpredicate",
				"phu",
				"nguyen",
				"nguyentanphu@hotmail.com",
				new DateTime(1992, 5, 18),
				Gender.Male,
				DateTime.UtcNow
			);
			_mongoRepository.Setup(m => m.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(user));

			var result = await _sut.GetAsync(u => u.UserName == "withpredicate");

			result.Should().Be(user);
		}

		[Fact]
		public async Task AnyAsyncSuccess()
		{
			var id = Guid.NewGuid();
			_mongoRepository.Setup(m => m.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(true));

			var result = await _sut.AnyAsync(u => u.Id == id);

			result.Should().Be(true);
		}

		[Fact]
		public async Task FindAsync_WithPredicate_Success()
		{
			IReadOnlyList<User> users = new []
			{
				new User(
				Guid.NewGuid(),
				"nhuhuynh",
				"nhu",
				"huynh",
				"nhuhuynh@hotmail.com",
				new DateTime(1992, 5, 18),
				Gender.Male,
				DateTime.UtcNow
				),
				new User(
					Guid.NewGuid(),
					"phunguyen",
					"nhu",
					"nguyen",
					"nguyentanphu@hotmail.com",
					new DateTime(1992, 5, 18),
					Gender.Male,
					DateTime.UtcNow
				),
			};
			_mongoRepository.Setup(m => m.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(users));

			//var result = await _sut.FindAsync(u => u.FirstName == "nhu");

			//result.Should().HaveCount(2);
		}

		[Fact]
		public async Task AddAsyncSuccess()
		{
			var user = new User(
				Guid.NewGuid(),
				"nhuhuynh1",
				"nhu",
				"huynh",
				"nhuhuynh@hotmail.com",
				new DateTime(1992, 5, 18),
				Gender.Male,
				DateTime.UtcNow
			);
			_mongoRepository.Setup(m => m.AddAsync(user))
				.Returns(Task.CompletedTask);

			await _sut.AddAsync(user);

			_mongoRepository.Verify(m => m.AddAsync(user), Times.Once);
			_context.Users.Any(u => u.UserName == "nhuhuynh1").Should().BeTrue();
		}

		[Fact]
		public async Task AddRangeAsyncSuccess()
		{
			var users = new[]
			{
				new User(
					Guid.NewGuid(),
					"nhuhuynh2",
					"nhu",
					"huynh",
					"nhuhuynh@hotmail.com",
					new DateTime(1992, 5, 18),
					Gender.Male,
					DateTime.UtcNow
				),
				new User(
					Guid.NewGuid(),
					"phunguyen2",
					"nhu",
					"nguyen",
					"nguyentanphu@hotmail.com",
					new DateTime(1992, 5, 18),
					Gender.Male,
					DateTime.UtcNow
				),
			};
			_mongoRepository.Setup(m => m.AddRangeAsync(users))
				.Returns(Task.CompletedTask);

			await _sut.AddRangeAsync(users);

			_mongoRepository.Verify(m => m.AddRangeAsync(users), Times.Once);
			_context.Users.Any(u => u.UserName == "phunguyen2").Should().BeTrue();
			_context.Users.Any(u => u.UserName == "nhuhuynh2").Should().BeTrue();
		}

		[Fact]
		public async Task UpdateAsyncSuccess()
		{
			var existingUser = await _context.Users.FirstAsync(u => u.UserName == "nguyenvanA");
			existingUser.UpdateUserAdditionalInfo(
				"updatedKnownas", 
				"I'm sexy and I know it", 
				"everyone with love", 
				"mysself", 
				"Ho chi minh",
				"Vietname");

			_mongoRepository.Setup(m => m.UpdateAsync(existingUser))
				.Returns(Task.CompletedTask);

			await _sut.UpdateAsync(existingUser);

			_mongoRepository.Verify(m => m.UpdateAsync(existingUser), Times.Once());
			_context.Users.Any(u => u.KnownAs == "updatedKnownas").Should().BeTrue();
		}
	}
}
