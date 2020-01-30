using FlirtingApp.Application.Bus;

namespace FlirtingApp.Application.Common.Interfaces.Bus
{
	public interface IBus
	{
		void Publish<TMessage>(TMessage message) where TMessage : IMessage;
	}
}