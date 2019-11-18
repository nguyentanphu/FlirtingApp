using System;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IAppUserManager
	{
		Task<Guid> CreateUserAsync(string userName, string password);
		Task<(bool Success, Guid UserId)> LoginUserAsync(string userName, string password, string refreshToken, string remoteIpAddress);
		Task<bool> HasValidRefreshTokenAsync(string refreshToken, Guid appUserId, string remoteIpAddress);
	}
}
