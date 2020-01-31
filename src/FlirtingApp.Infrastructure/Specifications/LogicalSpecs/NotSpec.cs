using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FlirtingApp.Infrastructure.Specifications.LogicalSpecs
{
	public sealed class NotSpec<T> : Specification<T> where T : new()
	{
		private readonly Specification<T> _spec;

		public NotSpec(Specification<T> spec)
		{
			_spec = spec;
		}
		public override Expression<Func<T, bool>> ToExpression()
		{
			var exp = _spec.ToExpression();
			var notBody = Expression.Not(exp);
			return Expression.Lambda<Func<T, bool>>(notBody, exp.Parameters.Single());
		}
	}
}
