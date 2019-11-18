using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	class AppIdentityDbContextFactory: DesignTimeDbContextFactoryBase<AppIdentityDbContext>
	{
		protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
		{
			return new AppIdentityDbContext(options);
		}
	}
}
