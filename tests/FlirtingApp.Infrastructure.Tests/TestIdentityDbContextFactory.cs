using System;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Infrastructure.Identity;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FlirtingApp.Infrastructure.Tests
{
	class TestIdentityDbContextFactory
	{
		public static Guid DefaultId = Guid.Parse("ba977a34-98b4-4043-8abd-9749a1911b95");
		public static string DefaultUserName = "phunguyen";
		public static string DefaultIp = "192.168.1.1";
		public static string DefaultRefreshToken = "default refresh token";
		public static IdentityDbContext Create()
		{
			var options = new DbContextOptionsBuilder<IdentityDbContext>()
				.UseInMemoryDatabase(nameof(IdentityDbContext))
				.Options;


			var context = new IdentityDbContext(options);
			context.Database.EnsureCreated();

			var user = new SecurityUser
			{
				Id = DefaultId,
				UserName = DefaultUserName,
				Email = "nguyentanphu@hotmail.com",
				PasswordHash = "bddadsadsa"
			};
			user.AddRefreshToken(DefaultRefreshToken, DefaultIp);

			context.AppUsers.Add(user);

			context.SaveChanges();

			return context;
		}
	}
}
