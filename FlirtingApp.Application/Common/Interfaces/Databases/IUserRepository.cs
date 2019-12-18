using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IUserRepository
	{
		Task<User> GetAsync(Guid id);
		Task<User> GetAsync(Expression<Func<User, bool>> predicate);
		Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);
		Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
		Task AddAsync(User user);
		Task AddRangeAsync(IEnumerable<User> users);
		Task UpdateAsync(User user);
	}
}
