using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Domain.Common;
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

			context.Users.AddRange(new[]
			{
				new User(
					Guid.NewGuid(),
					"nguyenvanA",
					"Mark",
					"Kenn",
					"nguyentanphu@hotmail.com",
					new DateTime(1992, 5, 18),
					Gender.Male,
					DateTime.UtcNow
				),
				new User(
					Guid.NewGuid(),
					"nguyenvanB",
					"sd",
					"fdfd",
					"nguyentanphu123@hotmail.com",
					new DateTime(1995, 5, 18),
					Gender.Female,
					DateTime.UtcNow
				),
			});

			context.SaveChanges();

			return context;
		}
	}
}
