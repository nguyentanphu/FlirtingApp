using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Persistent
{
	public class AppDbContext: DbContext, IAppDbContext
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

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
			{
				//switch (entry.State)
				//{
				//	case EntityState.Added:
				//		//entry.Entity.CreatedBy = _currentUserService.UserId;
				//		entry.Entity.Created = _dateTime.Now;
				//		break;
				//	case EntityState.Modified:
				//		entry.Entity.LastModifiedBy = _currentUserService.UserId;
				//		entry.Entity.LastModified = _dateTime.Now;
				//		break;
				//}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
