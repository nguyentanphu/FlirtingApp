using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FlirtingApp.Infrastructure.Specifications.LogicalSpecs
{
	internal sealed class AndSpec<T>: Specification<T> where T: new()
	{
		private readonly Specification<T> _left;
		private readonly Specification<T> _right;

		public AndSpec(Specification<T> left, Specification<T> right)
		{
			_left = left;
			_right = right;
		}

		public override Expression<Func<T, bool>> ToExpression()
		{
			var leftExp = _left.ToExpression();
			var rightExp = _right.ToExpression();

			var andExp = Expression.AndAlso(leftExp.Body, rightExp.Body);
			return Expression.Lambda<Func<T, bool>>(andExp, leftExp.Parameters.Single());
		}

		public override List<Expression<Func<T, object>>> Includes => _left.Includes.Concat(_right.Includes).ToList();
		public override List<string> IncludeStrings => _left.IncludeStrings.Concat(_right.IncludeStrings).ToList();
	}
}
