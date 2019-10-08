using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Dtos
{
	public class AccessToken
	{
		public string Token { get; }
		public int ExpiresIn { get; }

		public AccessToken(string token, int expiresIn)
		{
			Token = token;
			ExpiresIn = expiresIn;
		}
	}
}
