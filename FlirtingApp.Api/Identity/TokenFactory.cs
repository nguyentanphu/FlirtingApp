using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Identity
{
	public sealed class TokenFactory
	{
		public string GenerateToken(int size = 32)
		{
			var randomNumber = new byte[size];
			using (var randomNumGenerator = RandomNumberGenerator.Create())
			{
				randomNumGenerator.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
	}
}
