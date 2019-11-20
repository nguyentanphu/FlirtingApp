using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand: IRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
	}

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
	{
		private readonly IAppUserManager _userManager;
		private readonly IAppDbContext _dbContext;
		private readonly IMachineDateTime _dateTime;

		public CreateUserCommandHandler(IAppUserManager userManager, IAppDbContext dbContext, IMachineDateTime dateTime)
		{
			_userManager = userManager;
			_dbContext = dbContext;
			_dateTime = dateTime;
		}

		public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			if (await _userManager.UserNameExistAsync(request.UserName))
			{
				throw new ResourceExistedException("AppUser", "UserName");
			}
			if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
			{
				throw new ResourceExistedException("User", "Email");
			}

			var appUserId = await _userManager.CreateUserAsync(request.UserName, request.Password);
			_dbContext.Users.Add(new User
			{
				IdentityId = appUserId,
				UserName = request.UserName,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				DateOfBirth = request.DateOfBirth,
				LastActive = _dateTime.UtcNow
			});
			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
