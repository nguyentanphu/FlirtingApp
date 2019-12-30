using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Persistent
{
	public class AppDbContext: DbContext, IAppDbContext
	{
		private readonly ICurrentUser _currentUser;
		private readonly IMachineDateTime _dateTime;
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser, IMachineDateTime dateTime): base(options)
		{
			_currentUser = currentUser;
			_dateTime = dateTime;
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Photo> Photos { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
			base.OnModelCreating(builder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
			{
				var entity = entry.Entity;
				switch (entry.State)
				{
					case EntityState.Added:
						entity.CreatedBy ??= _currentUser.UserId;
						entity.Created ??= _dateTime.UtcNow;
						break;
					case EntityState.Modified:
						entity.LastModifiedBy ??= _currentUser.UserId;
						entity.LastModified ??= _dateTime.Now;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		public async Task MigrateAsync(CancellationToken cancellationToken = default)
		{
			await Database.MigrateAsync(cancellationToken);
		}

		public async Task EnsureDeletedAsync(CancellationToken cancellationToken = default)
		{
			await Database.EnsureDeletedAsync(cancellationToken);
		}

		public new void Update<TEntity>(TEntity model) where TEntity : Entity
		{
			base.Update(model);
		}
	}
}
