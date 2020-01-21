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
	class MongoRepository<TEntity>: IMongoRepository<TEntity> where TEntity: Entity
	{
		public IMongoCollection<TEntity> Collection { get; }
		public MongoRepository(IMongoDatabase database)
		{
			Collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
		}

		public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await Collection.Find(predicate).ToListAsync();
		}

		public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return Collection.Find(predicate).AnyAsync();
		}

		public Task<TEntity> GetAsync(Guid id)
		{
			return GetAsync(e => e.Id == id);
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await Collection.Find(predicate).FirstOrDefaultAsync();
		}

		public async Task AddAsync(TEntity entity)
		{
			await Collection.InsertOneAsync(entity);
		}
		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await Collection.InsertManyAsync(entities);
		}

		public async Task DeleteAsync(Guid id)
		{
			await Collection.DeleteOneAsync(e => e.Id == id);
		}
		public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			await Collection.DeleteManyAsync(predicate);
		}

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await Collection.Find(predicate).AnyAsync();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
		}
	}

}
