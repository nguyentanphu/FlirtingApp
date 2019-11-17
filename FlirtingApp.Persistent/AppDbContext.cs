using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Persistent
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{
			
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Photo> Photos { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}
	}
}
