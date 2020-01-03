using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlirtingApp.Application.Users.Queries.GetUsers
{
	public class GetUsersQueryValidator: AbstractValidator<GetUsersQuery>
	{
		public GetUsersQueryValidator()
		{
			RuleFor(q => q.Coordinates).Must(c => c.Length == 2).Unless(q => q.Coordinates == null);
		}
	}
}
