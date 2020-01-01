using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces.Databases;
using MediatR;

namespace FlirtingApp.Application.Users.Queries.GetUsers
{
	public class GetUsersQuery: IRequest<IEnumerable<UserOverviewDto>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		public double[] Coordinates { get; set; }
		public double Distance { get; set; }
	}

	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserOverviewDto>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserOverviewDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await _userRepository.FindAsync(request);

			return _mapper.Map<IEnumerable<UserOverviewDto>>(users);
		}
	}
}
