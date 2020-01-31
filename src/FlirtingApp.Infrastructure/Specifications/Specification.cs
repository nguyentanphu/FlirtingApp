using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FlirtingApp.Infrastructure.Specifications.LogicalSpecs;

namespace FlirtingApp.Infrastructure.Specifications
{
	public abstract class Specification<T> where T: new()
	{
		public abstract Expression<Func<T, bool>> ToExpression();

		public bool IsSatisfiedBy(T entity)
		{
			var predicate = ToExpression().Compile();
			return predicate(entity);
		}

		public static readonly Specification<T> All = new IdentitySpec<T>();

		public Specification<T> And(Specification<T> rightSpec)
		{
			return new AndSpec<T>(this, rightSpec);
		}
		public Specification<T> Or(Specification<T> rightSpec)
		{
			return new OrSpec<T>(this, rightSpec);
		}
		public Specification<T> Not()
		{
			return new NotSpec<T>(this);
		}

		public virtual List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
		public virtual List<string> IncludeStrings { get; } = new List<string>();
		public void AddInclude(Expression<Func<T, object>> include)
		{
			Includes.Add(include);
		}
		public void AddInclude(string include)
		{
			IncludeStrings.Add(include);
		}
	}
}
