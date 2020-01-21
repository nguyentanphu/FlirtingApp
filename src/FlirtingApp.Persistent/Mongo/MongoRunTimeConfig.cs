using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlirtingApp.Persistent.Mongo
{
	class MongoRunTimeConfig: IDbRunTimeConfig
	{
		private readonly IMongoDatabase _database;
		public MongoRunTimeConfig(IMongoDatabase database)
		{
			_database = database;
		}
		public async Task CreateLocationIndex()
		{
			var builder = Builders<User>.IndexKeys;
			var indexModel = new CreateIndexModel<User>(builder.Geo2DSphere(u => u.Location));
			await _database.GetCollection<User>(nameof(User)).Indexes
				.CreateOneAsync(indexModel);
		}
	}
}
