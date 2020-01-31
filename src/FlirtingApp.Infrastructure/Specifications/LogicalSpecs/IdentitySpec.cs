using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FlirtingApp.Infrastructure.Specifications.LogicalSpecs
{
	internal sealed class IdentitySpec<T>: Specification<T> where T: new()
	{
		public override Expression<Func<T, bool>> ToExpression()
		{
			return u => true;
		}
	}
}
