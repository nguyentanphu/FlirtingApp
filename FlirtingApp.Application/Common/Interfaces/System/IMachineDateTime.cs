using System;

namespace FlirtingApp.Application.Common.Interfaces.System
{
	public interface IMachineDateTime
	{
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}
}
