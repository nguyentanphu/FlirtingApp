using System;
using System.Security.Cryptography;

namespace FlirtingApp.Web.Identity
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
