using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FlirtingApp.Infrastructure.Identity.Models;

namespace FlirtingApp.Infrastructure.Specifications
{
	public sealed class SecurityUserSpec: Specification<SecurityUser>
	{
		private readonly Guid _securityUserId;
		public SecurityUserSpec(Guid securityUserId)
		{
			_securityUserId = securityUserId;
			AddInclude(u => u.RefreshTokens);
		}

		public override Expression<Func<SecurityUser, bool>> ToExpression()
		{
			return u => u.Id == _securityUserId;
		}
	}
}
