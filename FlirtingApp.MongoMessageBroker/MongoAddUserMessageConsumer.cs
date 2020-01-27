using FlirtingApp.Domain.Entities;
using FlirtingApp.Infrastructure.RabbitMq;
using FlirtingApp.Persistent.Mongo;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FlirtingApp.Infrastructure.MongoMessageBroker
{
	public class MongoAddUserMessageConsumer : BackgroundService
	{
		private readonly RabbitMQClient _rabbitMQClient;
		private readonly IMongoRepository<User> _mongoRepo;
		private const string QUEUE = "mongo-db-add-user";

		public MongoAddUserMessageConsumer(
			RabbitMQClient rabbitMQClient,
			IMongoRepository<User> mongoRepo)
		{
			_rabbitMQClient = rabbitMQClient;
			_mongoRepo = mongoRepo;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_rabbitMQClient.Consume(QUEUE, (message) =>
			{
				var user = JsonConvert.DeserializeObject<User>(message, CustomJsonSerializerSettings.GetSerializerSettings());

				var userCollection = _mongoRepo.AddAsync(user);
			});

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_rabbitMQClient.Dispose();
			base.Dispose();
		}
	}
}