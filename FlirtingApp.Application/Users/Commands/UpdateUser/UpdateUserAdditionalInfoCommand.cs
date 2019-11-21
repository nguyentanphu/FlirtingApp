using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Requests;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Users.Commands.UpdateUser
{
	public class UpdateUserAdditionalInfoCommand: UpdateUserAdditionalInfoRequest, IRequest
	{
		public Guid UserId { get; set; }
	}

	public class UpdateUserAdditionalInfoCommandHandler : IRequestHandler<UpdateUserAdditionalInfoCommand>
	{
		private readonly IAppDbContext _context;

		public UpdateUserAdditionalInfoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(UpdateUserAdditionalInfoCommand request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FindAsync(request.UserId);
			if (user == null)
			{
				throw new ResourceExistedException(nameof(User), nameof(User.UserId));
			}
			user.Introduction = request.Introduction;
			user.LookingFor = request.LookingFor;
			user.Interests = request.Interests;
			user.City = request.City;
			user.Country = request.Country;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
