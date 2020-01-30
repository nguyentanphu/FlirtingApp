using FlirtingApp.Application.Bus;
using FlirtingApp.Application.Common.Interfaces.Bus;
using FlirtingApp.Infrastructure.RabbitMq;

namespace MongoMessageBroker
{
	public class MongoMessagePublisher : IBus
	{
		private readonly RabbitMQClient _rabbitMQClient;

		public MongoMessagePublisher(RabbitMQClient rabbitMQClient)
		{
			_rabbitMQClient = rabbitMQClient;
		}

		public void Publish<TMessage>(TMessage message) where TMessage : IMessage
		{
			_rabbitMQClient.Publish(message.Body, message.Queue);
		}
	}
}