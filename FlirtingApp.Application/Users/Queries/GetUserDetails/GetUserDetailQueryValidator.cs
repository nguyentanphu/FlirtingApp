using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlirtingApp.Application.Users.Queries.GetUserDetails
{
	public class GetUserDetailQueryValidator: AbstractValidator<GetUserDetailQuery>
	{
		public GetUserDetailQueryValidator()
		{
			RuleFor(u => u.Id).NotEmpty();
		}
	}
}
