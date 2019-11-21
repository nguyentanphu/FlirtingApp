using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Users.Queries.GetUsers
{
	public class GetUsersQuery: IRequest<IEnumerable<UserOverviewDto>>
	{
	}

	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserOverviewDto>>
	{
		private readonly IAppDbContext _context;
		private readonly IMapper _mapper;

		public GetUsersQueryHandler(IAppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserOverviewDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await  _context.Users
				.Include(u => u.Photos)
				.ToArrayAsync(cancellationToken);

			return _mapper.Map<IEnumerable<UserOverviewDto>>(users);
		}
	}
}
