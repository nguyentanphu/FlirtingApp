using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface IAppIdentityDbContext
	{
		Task MigrateAsync(CancellationToken cancellationToken = default);
	}
}
