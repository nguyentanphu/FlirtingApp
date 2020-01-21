using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface ICurrentUser
	{
		Guid? UserId { get; set; }
		Guid? SecurityUserId { get; set; }
	}
}
