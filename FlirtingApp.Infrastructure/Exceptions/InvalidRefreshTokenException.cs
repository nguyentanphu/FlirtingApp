using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Infrastructure.Exceptions
{
	class InvalidRefreshTokenException: Exception
	{
		public InvalidRefreshTokenException(string message = "Invalid or expired refresh token"): base(message)
		{
			
		}
	}
}
