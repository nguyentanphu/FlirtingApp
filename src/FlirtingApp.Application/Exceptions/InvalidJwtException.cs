using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class InvalidJwtException: Exception
	{
		public InvalidJwtException(string message = "Invalid jwt token", Exception? innerException = null): base(message, innerException)
		{
			
		}
	}
}
