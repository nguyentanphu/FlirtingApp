using System.Security.Claims;
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
			var identityString = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(identityString, out var result))
			{
				UserId = result;
			}
		}

		public Guid? UserId { get; set; }
	}
}
