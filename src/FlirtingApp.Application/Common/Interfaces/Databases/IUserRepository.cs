using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Dtos;
using FlirtingApp.Application.Users.Queries.GetUsers;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IUserRepository
	{
		Task<User?> GetAsync(Guid id);
		Task<User?> GetAsync(Expression<Func<User, bool>> predicate);
		Task<IReadOnlyList<User>> FindAsync(GetUsersQuery query);
		Task<IReadOnlyList<UserWithDistanceDto>> FindWithGeoSpatial(GetUsersQuery query);
		Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
		Task AddAsync(User user);
		Task AddRangeAsync(IEnumerable<User> users);
		Task UpdateAsync(User user);
	}
}
