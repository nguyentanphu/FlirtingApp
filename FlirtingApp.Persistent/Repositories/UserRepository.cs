using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Dtos;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Users.Queries.GetUsers;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Persistent.Mongo;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace FlirtingApp.Persistent.Repositories
{
	class UserRepository: IUserRepository
	{
		private readonly AppDbContext _sql;
		private readonly IMongoRepository<User> _mongoRepository;

		public UserRepository(AppDbContext sql, IMongoRepository<User> mongoRepository)
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

		public Task<IReadOnlyList<User>> FindAsync(GetUsersQuery query)
		{
			return _mongoRepository.FindAsync(u => !query.Gender.HasValue || u.Gender == query.Gender);
		}

		public async Task<IReadOnlyList<UserWithDistanceDto>> FindWithGeoSpatial(GetUsersQuery query)
		{
			//var gp = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(query.Coordinates[0], query.Coordinates[1]));
			//var filter = Builders<User>.Filter.Near(u => u.Location, gp, query.Distance);
			//var raw = await _mongoRepository.Collection.Find(filter).ToListAsync();

			if (query.Coordinates == null)
			{
				throw new ArgumentException("This method needs a coordinates", nameof(query.Coordinates));
			}

			var queryCriteria = new BsonDocument();
			if (query.Gender.HasValue)
			{
				queryCriteria.AddRange(new BsonDocument { {"gender", query.Gender} });
			}
			var geoNearOptions = new BsonDocument
			{
				{
					"near", new BsonDocument
					{
						{"type", "Point"},
						{"coordinates", new BsonArray(query.Coordinates)}
					}
				},
				{"maxDistance", query.Distance},
				{"query", queryCriteria },
				{"distanceField", "distance"}
			};
			var geoNearAggregate = new BsonDocumentPipelineStageDefinition<User, UserWithDistanceDto>(new BsonDocument
			{
				{ "$geoNear", geoNearOptions }
			});

			return await _mongoRepository.Collection.Aggregate()
				.AppendStage(geoNearAggregate)
				.ToListAsync();
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
