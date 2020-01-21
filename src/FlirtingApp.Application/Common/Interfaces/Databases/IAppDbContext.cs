using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IAppDbContext
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
		Task MigrateAsync(CancellationToken cancellationToken = default);
		Task EnsureDeletedAsync(CancellationToken cancellationToken = default);
		void Update<TEntity>(TEntity model) where TEntity : Entity;
	}
}
