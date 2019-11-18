using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	

	class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IAppIdentityDbContext
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
		{
		}

		public DbSet<AppUser> AppUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);
		}

		public async Task MigrateAsync(CancellationToken cancellationToken = default)
		{
			await Database.MigrateAsync(cancellationToken);
		}
	}
}
