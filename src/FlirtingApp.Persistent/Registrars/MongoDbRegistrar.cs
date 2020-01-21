using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Utils;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Persistent.ConfigOptions;
using FlirtingApp.Persistent.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FlirtingApp.Persistent.Registrars
{
	static class MongoDbRegistrar
	{
		public static IServiceCollection AddMongoDb(this IServiceCollection services)
		{
			services.AddSingleton<MongoOptions>(sp =>
			{
				var configuration = sp.GetRequiredService<IConfiguration>();
				return configuration.GetOptions<MongoOptions>(nameof(MongoOptions));
			});

			services.AddSingleton<IMongoClient>(sp =>
			{
				var mongoOptions = sp.GetRequiredService<MongoOptions>();
				return new MongoClient(mongoOptions.ConnectionString);
			});

			services.AddScoped<IMongoDatabase>(sp =>
			{
				var client = sp.GetRequiredService<IMongoClient>();
				var mongoOptions = sp.GetRequiredService<MongoOptions>();
				return client.GetDatabase(mongoOptions.Database);
			});

			services.AddScoped<IDbRunTimeConfig, MongoRunTimeConfig>();

			return services;
		}

		public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
		{
			AddMongoClassConfiguration();
			services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

			return services;
		}

		private static void AddMongoClassConfiguration()
		{
			var conventionPack = new ConventionPack {new CamelCaseElementNameConvention()};
			ConventionRegistry.Register("camelCase", conventionPack, t => true);

			BsonClassMap.RegisterClassMap<User>(cm =>
			{
				cm.AutoMap();
				cm.MapField("_photos").SetElementName("photos");
			});
			BsonClassMap.RegisterClassMap<Photo>(cm =>
			{
				cm.AutoMap();
			});
		}
	}
}
