﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace FlirtingApp.Infrastructure.RabbitMq
{
	public class RabbitMQClient : IDisposable
	{
		private readonly IModel _channel;
		private readonly IConnection _connection;

		public RabbitMQClient(RabbitMQConfig rabbitMQConfig)
		{
			var factory = new ConnectionFactory()
			{
				HostName = rabbitMQConfig.HostName,
			};
			// Rabbitmq documentation it's ok to reuse connection. Close and reopen connection doesn't actually make a difference
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public void Publish(string message, string queue)
		{
			// This queue declare here is idempotent
			_channel.QueueDeclare(
				queue: queue,
				durable: true,
				exclusive: false,
				autoDelete: false);

			var encodedMessage = EncodeUTF8(message);

			_channel.BasicPublish(
				exchange: "",
				routingKey: queue,
				basicProperties: null,
				body: encodedMessage);
		}

		public void Consume(string queue, Action<string> action)
		{
			_channel.QueueDeclare(
				queue: queue,
				durable: true,
				exclusive: false,
				autoDelete: false);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (ch, ea) =>
			{
				var content = DecodeUTF8(ea.Body);
				action(content);
			};

			_channel.BasicConsume(queue: queue,
							 autoAck: true,
							 consumer: consumer);
		}

		private string DecodeUTF8(byte[] body)
		{
			return System.Text.Encoding.UTF8.GetString(body);
		}

		public void Dispose()
		{
			_connection.Close();
			_channel.Close();
		}

		private byte[] EncodeUTF8(string message)
		{
			return Encoding.UTF8.GetBytes(message);
		}
	}
}