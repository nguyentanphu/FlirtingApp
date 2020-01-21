using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace FlirtingApp.Application.Exceptions
{
	public class AppValidationException: Exception
	{
		public AppValidationException(IEnumerable<ValidationFailure> errors)
		{
			var failures = errors
				.GroupBy(e => e.PropertyName)
				.Select(g => new
				{
					// to camel case by lower first letter
					PropName = $"{char.ToLowerInvariant(g.Key[0])}{g.Key.Substring(1)}",
					Errors = g.Select(e => e.ErrorMessage).ToArray()
				})
				.ToDictionary(x => x.PropName, x => x.Errors);
			Failures = failures;
		}

		public IDictionary<string, string[]> Failures;
	}
}
