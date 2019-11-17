using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface IAppUserManager
	{
		Task<Guid> CreateUserAsync(string userName, string password);
		Task<bool> HasValidRefreshToken(string refreshToken, Guid appUserId, string remoteIpAddress);
		Task MigrateIdentityDb();
	}
}
