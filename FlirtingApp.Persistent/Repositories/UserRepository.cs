using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Persistent.Repositories
{
	class UserRepository: IUserRepository
	{
		//private readonly IAppDbContext
		public UserRepository()
		{
			
		}
		public Task<User> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetAsync(Expression<Func<User, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(User user)
		{
			throw new NotImplementedException();
		}
	}
}
