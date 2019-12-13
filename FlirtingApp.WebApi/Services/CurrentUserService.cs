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

			var securityUserIdString = httpContextAccessor.HttpContext.User.FindFirstValue(AppClaimTypes.SecurityUserId);
			if (Guid.TryParse(securityUserIdString, out var securityUserId))
			{
				SecurityUserId = securityUserId;
			}
		}

		public Guid? UserId { get; set; }
		public Guid? SecurityUserId { get; set; }
	}
}
