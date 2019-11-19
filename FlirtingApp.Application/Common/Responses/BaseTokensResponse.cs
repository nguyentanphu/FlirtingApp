using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Common.Responses
{
	public class BaseTokensResponse
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
