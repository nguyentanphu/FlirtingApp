using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
			var user = new User
			{
				//Id = Guid.NewGuid(),
				IdentityId = Guid.NewGuid(),
				UserName = "phunguyen"
			};
			_mongoRepository.Setup(m => m.GetAsync(id))
				.Returns(Task.FromResult(user));

			var result = await _sut.GetAsync(id);

			result.Should().Be(user);
		}
		[Fact]
		public async Task GetAsync_WithPredicate_Success()
		{
			var id = Guid.NewGuid();
			var user = new User
			{
				//Id = Guid.NewGuid(),
				IdentityId = Guid.NewGuid(),
				UserName = "phunguyen"
			};
			_mongoRepository.Setup(m => m.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(user));

			var result = await _sut.GetAsync(u => u.Id == id);

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
			var users = new []
			{
				new User
				{
					//Id = Guid.NewGuid(),
					IdentityId = Guid.NewGuid(),
					UserName = "phunguyen",
					LastName = "Nguyen"
				},
				new User
				{
					//Id = Guid.NewGuid(),
					IdentityId = Guid.NewGuid(),
					UserName = "nhunguyen",
					LastName = "Nguyen"
				}
			};
			_mongoRepository.Setup(m => m.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.Returns(Task.FromResult(users.AsEnumerable()));

			var result = await _sut.FindAsync(u => u.LastName == "Nguyen");

			result.Should().HaveCount(2);
		}

		[Fact]
		public async Task AddAsyncSuccess()
		{
			var user = new User
			{
				//Id = Guid.NewGuid(),
				IdentityId = Guid.NewGuid(),
				UserName = "phunguyen1"
			};
			_mongoRepository.Setup(m => m.AddAsync(user))
				.Returns(Task.CompletedTask);

			await _sut.AddAsync(user);

			_mongoRepository.Verify(m => m.AddAsync(user), Times.Once);
			_context.Users.Any(u => u.UserName == "phunguyen1").Should().BeTrue();
		}

		[Fact]
		public async Task AddRangeAsyncSuccess()
		{
			var users = new []
			{
				new User
				{
					//Id = Guid.NewGuid(),
					IdentityId = Guid.NewGuid(),
					UserName = "phunguyen2"
				},
				new User
				{
					//Id = Guid.NewGuid(),
					IdentityId = Guid.NewGuid(),
					UserName = "nhuhuynh2"
				}
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
			var id = Guid.Parse("b59d73a3-5664-400d-a5b2-a480de818919");
			var updateUser = new User
			{
				//Id = id,
				IdentityId = Guid.NewGuid(),
				UserName = "phunguyen4"
			};
			_mongoRepository.Setup(m => m.UpdateAsync(updateUser))
				.Returns(Task.CompletedTask);
			var localUser = _context.Users.Local.First(u => u.Id == id);
			_context.Entry(localUser).State = EntityState.Detached;

			await _sut.UpdateAsync(updateUser);

			_mongoRepository.Verify(m => m.UpdateAsync(updateUser), Times.Once());
			_context.Users.Any(u => u.UserName == "phunguyen4").Should().BeTrue();
		}
	}
}
