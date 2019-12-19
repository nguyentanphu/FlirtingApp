using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Users.Queries.GetUserDetails
{
	public class GetUserDetailQuery: IRequest<UserDetailDto>
	{
		public Guid Id { get; set; }
	}

	public class GetUserDetailQueryHandler: IRequestHandler<GetUserDetailQuery, UserDetailDto>
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<UserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetAsync(request.Id);
			if (user == null)
			{
				throw new ResourceNotFoundException("User", request.Id);
			}

			return _mapper.Map<UserDetailDto>(user);

		}
	}
}
