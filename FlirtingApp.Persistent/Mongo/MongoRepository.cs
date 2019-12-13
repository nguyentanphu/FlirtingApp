using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Common;
using MongoDB.Driver;

namespace FlirtingApp.Persistent.Mongo
{
	class MongoRepository<TEntity>: IMongoRepository<TEntity> where TEntity: AuditableEntity
	{
		public readonly IMongoCollection<TEntity> _collection;
		public MongoRepository(IMongoDatabase database)
		{
			database.GetCollection<TEntity>(nameof(TEntity));
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _collection.Find(predicate).ToListAsync();
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _collection.Find(predicate).FirstOrDefaultAsync();
		}

		public async Task AddAsync(TEntity entity)
		{
			await _collection.InsertOneAsync(entity);
		}

		public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
		{
			await _collection.DeleteOneAsync(predicate);
		}

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _collection.Find(predicate).AnyAsync();
		}

		public Task UpdateAsync(TEntity entity)
		{
			_collection.update
		}
	}
	{
	}
}
