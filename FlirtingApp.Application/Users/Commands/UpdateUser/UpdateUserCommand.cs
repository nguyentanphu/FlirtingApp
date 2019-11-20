using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Users.Commands.UpdateUser
{
	public class UpdateUserAdditionalInfoCommand: IRequest
	{
		public Guid UserId { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}

	public class UpdateUserAdditionalInfoCommandHandler : IRequestHandler<UpdateUserAdditionalInfoCommand>
	{
		private readonly IAppDbContext _context;

		public UpdateUserAdditionalInfoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateUserAdditionalInfoCommand request, CancellationToken cancellationToken)
		{
			var user = _context.Users.SingleOrDefaultAsync(u => u.)
		}
	}
}
