using FlirtingApp.Application.Bus;

namespace FlirtingApp.Application.Common.Interfaces.Bus
{
	public class MongoAddUserMessage : IMessage
	{
		public MongoAddUserMessage(string body)
		{
			Body = body;
		}

		public string Body { get; }

		public string Queue => "mongo-db-add-user";
	}
}