using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Users.Commands.Login
{
	public class LoginCommandReponse
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
