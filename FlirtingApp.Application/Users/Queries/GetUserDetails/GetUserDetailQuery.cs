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
		public Guid UserId { get; set; }
	}

	public class GetUserDetailQueryHandler: IRequestHandler<GetUserDetailQuery, UserDetailDto>
	{
		private readonly IAppDbContext _context;
		private readonly IMapper _mapper;
		public GetUserDetailQueryHandler(IAppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<UserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users
				.Include(u => u.Photos)
				.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
			if (user == null)
			{
				throw new ResourceNotFoundException("User", request.UserId);
			}

			return _mapper.Map<UserDetailDto>(user);

		}
	}
}
