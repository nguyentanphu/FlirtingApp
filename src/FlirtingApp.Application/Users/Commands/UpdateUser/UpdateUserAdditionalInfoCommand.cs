using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Entities;
using MediatR;

namespace FlirtingApp.Application.Users.Commands.UpdateUser
{
	public class UpdateUserAdditionalInfoCommand: UpdateUserAdditionalInfoRequest, IRequest
	{
		public Guid UserId { get; set; }
	}

	public class UpdateUserAdditionalInfoCommandHandler : IRequestHandler<UpdateUserAdditionalInfoCommand>
	{
		private readonly IUserRepository _userRepository;

		public UpdateUserAdditionalInfoCommandHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Unit> Handle(UpdateUserAdditionalInfoCommand request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetAsync(request.UserId);
			if (user == null)
			{
				throw new ResourceExistedException(nameof(User), nameof(User.Id));
			}

			user.UpdateUserAdditionalInfo(
				request.KnownAs,
				request.Introduction,
				request.LookingFor,
				request.Interests,
				request.City,
				request.Country
			);

			await _userRepository.UpdateAsync(user);

			return Unit.Value;
		}
	}
}
