using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Persistent.Mongo;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Persistent.Repositories
{
	class UserRepository: IUserRepository
	{
		private readonly IAppDbContext _sql;
		private readonly IMongoRepository<User> _mongoRepository;

		public UserRepository(IAppDbContext sql, IMongoRepository<User> mongoRepository)
		{
			_sql = sql;
			_mongoRepository = mongoRepository;
		}
		public Task<User> GetAsync(Guid id)
		{
			return _mongoRepository.GetAsync(id);
		}

		public Task<User> GetAsync(Expression<Func<User, bool>> predicate)
		{
			return _mongoRepository.GetAsync(predicate);
		}

		public Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
		{
			return _mongoRepository.AnyAsync(predicate);
		}

		public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
		{
			return _mongoRepository.FindAsync(predicate);
		}

		public async Task AddAsync(User user)
		{
			_sql.Users.Add(user);
			await _sql.SaveChangesAsync();
			await _mongoRepository.AddAsync(user);
		}

		public async Task AddRangeAsync(IEnumerable<User> users)
		{
			_sql.Users.AddRange(users);
			await _sql.SaveChangesAsync();
			await _mongoRepository.AddRangeAsync(users);
		}

		public async Task UpdateAsync(User user)
		{
			_sql.Update(user);
			await _sql.SaveChangesAsync();
			await _mongoRepository.UpdateAsync(user);
		}

	}
}
