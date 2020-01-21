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
	

	public class IdentityDbContext : IdentityDbContext<SecurityUser, AppRole, Guid>, IIdentityDbContext
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base(options)
		{
		}

		public DbSet<SecurityUser> AppUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
			base.OnModelCreating(builder);
		}

		public async Task MigrateAsync(CancellationToken cancellationToken = default)
		{
			await Database.MigrateAsync(cancellationToken);
		}
	}
}
