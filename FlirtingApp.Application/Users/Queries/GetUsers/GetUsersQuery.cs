using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using MediatR;

namespace FlirtingApp.Application.Users.Queries.GetUsers
{
	public class GetUsersQuery: IRequest<GetUsersQueryResponse>
	{
	}

	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
	{
		private readonly IAppDbContext _context;

		public GetUsersQueryHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = _context.Users
		}
	}
}
