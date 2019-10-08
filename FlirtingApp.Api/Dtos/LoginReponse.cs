using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Dtos
{
	public class LoginReponse
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public bool Success { get; set; }
	}
}
