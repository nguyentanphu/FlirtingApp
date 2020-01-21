using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Infrastructure.Exceptions
{
	class CreateAppUserException: Exception
	{
		public CreateAppUserException(string message): base(message)
		{
			
		}
	}
}
