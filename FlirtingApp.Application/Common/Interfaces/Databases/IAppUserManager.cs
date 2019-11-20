using System;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IAppUserManager
	{
		Task<bool> UserNameExistAsync(string userName);
		Task<Guid> CreateUserAsync(string userName, string password);
		Task<(bool Success, Guid AppUserId, string RefreshToken)> LoginUserAsync(string userName, string password, string remoteIpAddress);
		Task<bool> HasValidRefreshTokenAsync(string refreshToken, Guid appUserId, string remoteIpAddress);
		Task<string> ExchangeRefreshTokenAsync(Guid appUserId, string refreshToken, string remoteIpAddress);
	}
}
