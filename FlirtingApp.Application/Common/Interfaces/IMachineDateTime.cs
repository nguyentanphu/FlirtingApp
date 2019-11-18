using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface IMachineDateTime
	{
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}
}
