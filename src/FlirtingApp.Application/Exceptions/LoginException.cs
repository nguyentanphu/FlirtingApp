using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application
{
	public class LoginException: Exception
	{
		public LoginException(string message = "Login failed. Either user name or password is not correct"): base(message)
		{

		}
	}
}
