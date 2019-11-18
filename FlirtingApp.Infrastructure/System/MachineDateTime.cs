using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Application.Common.Interfaces;

namespace FlirtingApp.Infrastructure.System
{
	class MachineDateTime : IMachineDateTime
	{
		public DateTime Now => DateTime.Now;
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
