using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using MediatR;

namespace FlirtingApp.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand: RequestBase<CreateUserCommandResponse>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Gender Gender { get; set; }
	}
	public class CreateUserCommandResponse: ResponseBase { }

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
	{
		private readonly ISecurityUserManager _userManager;
		private readonly IUserRepository _userRepository;
		private readonly IMachineDateTime _dateTime;

		public CreateUserCommandHandler(ISecurityUserManager userManager, IUserRepository userRepository, IMachineDateTime dateTime)
		{
			_userManager = userManager;
			_userRepository = userRepository;
			_dateTime = dateTime;
		}

		public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			if (await _userManager.UserNameExistAsync(request.UserName))
			{
				request.OutputPort.Handle(new CreateUserCommandResponse
				{
					Success = false,
					ErrorMessage = "User name is not available"
				});
				return Unit.Value;
			}
			if (await _userRepository.AnyAsync(u => u.Email == request.Email))
			{
				request.OutputPort.Handle(new CreateUserCommandResponse
				{
					Success = false,
					ErrorMessage = "Email is not available"
				});
				return Unit.Value;
			}

			var securityUserId = await _userManager.CreateUserAsync(request.UserName, request.Password);

			await _userRepository.AddAsync(new User
			{
				IdentityId = securityUserId,
				UserName = request.UserName,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				DateOfBirth = request.DateOfBirth,
				Gender = request.Gender,
				LastActive = _dateTime.UtcNow,
			});

			request.OutputPort.Handle(new CreateUserCommandResponse
			{
				Success = true
			});
			return Unit.Value;
		}
	}
}
