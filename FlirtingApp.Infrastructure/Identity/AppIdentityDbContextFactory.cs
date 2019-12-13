using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	class AppIdentityDbContextFactory: DesignTimeDbContextFactoryBase<IdentityDbContext>
	{
		protected override IdentityDbContext CreateNewInstance(DbContextOptions<IdentityDbContext> options)
		{
			return new IdentityDbContext(options);
		}
	}
}
