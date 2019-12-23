using System;
using FlirtingApp.Infrastructure.Identity;

namespace FlirtingApp.Infrastructure.Tests
{
	public class IdentityDbContextFixture: IDisposable
	{
		public IdentityDbContext Context { get; private set; }

		public IdentityDbContextFixture()
		{
			Context = TestIdentityDbContextFactory.Create();
		}
		public void Dispose()
		{
			Context.Database.EnsureDeleted();
			Context.Dispose();
		}
	}
}
