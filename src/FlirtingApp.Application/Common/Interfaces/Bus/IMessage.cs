namespace FlirtingApp.Application.Bus
{
	public interface IMessage
	{
		string Body { get; }
		string Queue { get; }
	}
}