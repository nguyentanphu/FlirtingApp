using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Persistent
{
	class AppDbContextFactory: DesignTimeDbContextFactoryBase<AppDbContext>
	{
		protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
		{
			return new AppDbContext(options);
		}
	}
}
