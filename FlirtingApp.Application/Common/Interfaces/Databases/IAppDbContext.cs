using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IAppDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Photo> Photos { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
		Task MigrateAsync(CancellationToken cancellationToken = default);
		void Update<TEntity>(TEntity model) where TEntity : IIdentifiable;
	}
}
