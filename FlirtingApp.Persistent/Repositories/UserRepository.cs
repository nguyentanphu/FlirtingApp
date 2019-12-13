using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Persistent.Mongo;

namespace FlirtingApp.Persistent.Repositories
{
	class UserRepository: IUserRepository
	{
		private readonly IAppDbContext _sqlDb;
		private readonly IMongoRepository<User> _mongoRepository;

		public UserRepository(IAppDbContext sqlDb, IMongoRepository<User> mongoRepository)
		{
			_sqlDb = sqlDb;
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

		public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
		{
			return _mongoRepository.FindAsync(predicate);
		}

		public async Task AddAsync(User user)
		{
			_sqlDb.Users.Add(user);
			await _sqlDb.SaveChangesAsync();
			await _mongoRepository.AddAsync(user);
		}

		public async Task AddRangeAsync(IEnumerable<User> users)
		{
			_sqlDb.Users.AddRange(users);
			await _sqlDb.SaveChangesAsync();
			await _mongoRepository.AddRangeAsync(users);
		}

		public async Task UpdateAsync(User user)
		{
			_sqlDb.Update(user);
			await _sqlDb.SaveChangesAsync();
			await _mongoRepository.UpdateAsync(user);
		}
	}
}
