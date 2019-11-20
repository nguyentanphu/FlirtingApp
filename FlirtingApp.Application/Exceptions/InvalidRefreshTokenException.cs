using System;

namespace FlirtingApp.Application.Exceptions
{
	public class InvalidRefreshTokenException: Exception
	{
		public InvalidRefreshTokenException(string message = "Invalid or expired refresh token"): base(message)
		{
			
		}
	}
}
