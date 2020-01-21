using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace FlirtingApp.Application.Common.Behaviors
{
	public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var validationContext = new ValidationContext(request);

			var errors = _validators
				.Select(v => v.Validate(validationContext))
				.SelectMany(r => r.Errors)
				.ToArray();
			if (errors.Any())
			{
				throw new AppValidationException(errors);
			}

			return next();
		}
	}
}
