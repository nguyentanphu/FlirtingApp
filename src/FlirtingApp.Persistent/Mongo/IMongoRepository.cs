﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlirtingApp.Domain.Common;
using MongoDB.Driver;

namespace FlirtingApp.Persistent.Mongo
{
	public interface IMongoRepository<TEntity> where TEntity: Entity
	{
		IMongoCollection<TEntity> Collection { get; }
		Task<TEntity> GetAsync(Guid id);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
		Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
		//Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
		//	TQuery query) where TQuery : PagedQueryBase;
		Task AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(Guid id);
		Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
