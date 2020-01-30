using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Bus;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand : RequestBase<CreateUserCommandResponse>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Gender Gender { get; set; }
		public double[] Coordinates { get; set; }
	}

	public class CreateUserCommandResponse : ResponseBase
	{
		public Guid UserId { get; set; }
	}

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
	{
		private readonly ISecurityUserManager _userManager;
		private readonly IUserRepository _userRepository;
		private readonly IMachineDateTime _dateTime;
		private readonly IBus _bus;

		public CreateUserCommandHandler(
			ISecurityUserManager userManager,
			IUserRepository userRepository,
			IMachineDateTime dateTime,
			IBus bus)
		{
			_userManager = userManager;
			_userRepository = userRepository;
			_dateTime = dateTime;
			_bus = bus;
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

			var newUser = new User(
				securityUserId,
				request.UserName,
				request.FirstName,
				request.LastName,
				request.Email,
				request.DateOfBirth,
				request.Gender,
				_dateTime.UtcNow
			);

			newUser.SetLocation(request.Coordinates);

			await _userRepository.AddAsync(newUser);

			_bus.Publish(new MongoAddUserMessage(
					body: JsonConvert.SerializeObject(newUser)
			));

			request.OutputPort.Handle(new CreateUserCommandResponse
			{
				Success = true,
				UserId = newUser.Id
			});
			return Unit.Value;
		}
	}
}