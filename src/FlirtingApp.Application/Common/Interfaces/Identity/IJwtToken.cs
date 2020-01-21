using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FlirtingApp.Application.Common.Interfaces.Identity
{
	public interface IJwtFactory
	{
		string GenerateEncodedTokens(Guid userId, Guid securityUserId, string userName);
		ClaimsPrincipal GetClaimPrinciple(string accessToken);
	}
}
