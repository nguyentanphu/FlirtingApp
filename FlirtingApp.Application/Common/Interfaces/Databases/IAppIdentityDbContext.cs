using System.Threading;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IAppIdentityDbContext
	{
		Task MigrateAsync(CancellationToken cancellationToken = default);
	}
}
