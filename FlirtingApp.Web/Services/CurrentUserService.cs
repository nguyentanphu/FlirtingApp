using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Guid = System.Guid;

namespace FlirtingApp.Web.Services
{
	public class CurrentUserService: ICurrentUser
	{
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			var identityString = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(identityString, out var result))
			{
				UserId = result;
			}
		}

		public Guid? UserId { get; set; }
	}
}
