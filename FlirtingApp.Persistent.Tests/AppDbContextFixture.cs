using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Persistent.Tests
{
	public class AppDbContextFixture: IDisposable
	{
		public AppDbContext Context { get; private set; }

		public AppDbContextFixture()
		{
			Context = TestAppDbContextFactory.Create();
		}
		public void Dispose()
		{
			Context.Database.EnsureDeleted();
			Context.Dispose();
		}
	}
}
