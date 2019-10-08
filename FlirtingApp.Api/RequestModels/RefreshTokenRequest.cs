using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.RequestModels
{
	public class RefreshTokenRequest
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
