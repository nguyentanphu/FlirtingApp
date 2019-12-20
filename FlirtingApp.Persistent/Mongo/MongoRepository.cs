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
	class MongoRepository<TEntity>: IMongoRepository<TEntity> where TEntity: IIdentifiable
	{
		public readonly IMongoCollection<TEntity> _collection;
		public MongoRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _collection.Find(predicate).ToListAsync();
		}

		public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return _collection.Find(predicate).AnyAsync();
		}

		public Task<TEntity> GetAsync(Guid id)
		{
			return GetAsync(e => e.Id == id);
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _collection.Find(predicate).FirstOrDefaultAsync();
		}

		public async Task AddAsync(TEntity entity)
		{
			await _collection.InsertOneAsync(entity);
		}
		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await _collection.InsertManyAsync(entities);
		}

		public async Task DeleteAsync(Guid id)
		{
			await _collection.DeleteOneAsync(e => e.Id == id);
		}
		public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			await _collection.DeleteManyAsync(predicate);
		}

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _collection.Find(predicate).AnyAsync();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
		}
	}

}
