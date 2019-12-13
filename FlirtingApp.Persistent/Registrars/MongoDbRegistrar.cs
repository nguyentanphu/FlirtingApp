using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Utils;
using FlirtingApp.Domain.Entities;
using FlirtingApp.Persistent.ConfigOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
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

			services.AddSingleton<MongoClient>(sp =>
			{
				var mongoOptions = sp.GetRequiredService<MongoOptions>();
				return new MongoClient(mongoOptions.ConnectionString);
			});

			services.AddScoped<IMongoDatabase>(sp =>
			{
				var client = sp.GetRequiredService<MongoClient>();
				var mongoOptions = sp.GetRequiredService<MongoOptions>();
				return client.GetDatabase(mongoOptions.Database);
			});

			return services;
		}

		public static void AddMongoClassConfiguration()
		{
			BsonClassMap.RegisterClassMap<User>(cm =>
			{
				cm.AutoMap();
				cm.MapIdProperty(p => p.UserId);
				cm.MapField("_photos").SetElementName("Photos");
			});
			BsonClassMap.RegisterClassMap<Photo>(cm =>
			{
				cm.AutoMap();
				cm.MapIdProperty(m => m.PhotoId);
			});
		}
	}
}
