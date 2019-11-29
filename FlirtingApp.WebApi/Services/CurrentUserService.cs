using System.Security.Claims;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Guid = System.Guid;

namespace FlirtingApp.WebApi.Services
{
	public class CurrentUserService: ICurrentUser
	{
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			if (httpContextAccessor.HttpContext?.User == null)
			{
				return;
			}
			var userIdString = httpContextAccessor.HttpContext.User.FindFirstValue(AppClaimTypes.UserId);
			if (Guid.TryParse(userIdString, out var userId))
			{
				UserId = userId;
			}

			var appUserIdString = httpContextAccessor.HttpContext.User.FindFirstValue(AppClaimTypes.AppUserId);
			if (Guid.TryParse(appUserIdString, out var appUserId))
			{
				AppUserId = appUserId;
			}
		}

		public Guid? UserId { get; set; }
		public Guid? AppUserId { get; set; }
	}
}
