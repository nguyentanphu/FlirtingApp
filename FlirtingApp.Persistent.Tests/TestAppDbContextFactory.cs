using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FlirtingApp.Persistent.Tests
{
	class TestAppDbContextFactory
	{
		public static AppDbContext Create()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(nameof(AppDbContext))
				.Options;

			var currentUser = new Mock<ICurrentUser>();
			currentUser.SetupGet(c => c.UserId).Returns(Guid.NewGuid());
			currentUser.SetupGet(c => c.SecurityUserId).Returns(Guid.NewGuid());

			var machineDateTime = new Mock<IMachineDateTime>();
			machineDateTime.SetupGet(m => m.Now).Returns(DateTime.Now);
			machineDateTime.SetupGet(m => m.UtcNow).Returns(DateTime.UtcNow);
			var context = new AppDbContext(options, currentUser.Object, machineDateTime.Object);
			context.Database.EnsureCreated();

			//context.Users.AddRange(new[]
			//{
			//	new User
			//	{
			//		Id = Guid.Parse("b59d73a3-5664-400d-a5b2-a480de818919"),
			//		IdentityId = Guid.NewGuid(),
			//		UserName = "nguyenvanA",
			//		FirstName = "Mark",
			//		LastName = "Kenn"
			//	},
			//	new User
			//	{
			//		Id = Guid.Parse("03760e90-9bdb-4b99-9089-2361c3dc0e8b"),
			//		IdentityId = Guid.NewGuid(),
			//		UserName = "nguyenvanB",
			//		FirstName = "Oper",
			//		LastName = "Ops"
			//	}
			//});

			context.SaveChanges();

			return context;
		}
	}
}
