using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Domain.Common
{
	public interface IIdentifiable
	{
		Guid Id { get; set; }
	}
}
