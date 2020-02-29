using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.WebApi.RequestModels
{
	public class LoginRequest
	{
		public string UserName { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
