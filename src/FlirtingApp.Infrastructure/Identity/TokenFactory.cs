using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FlirtingApp.Application.Common.Interfaces;

namespace FlirtingApp.Infrastructure.Identity
{
	public sealed class TokenFactory: ITokenFactory
	{
		public string GenerateToken(int size = 32)
		{
			var randomNumber = new byte[size];
			using var randomNumGenerator = RandomNumberGenerator.Create();
			randomNumGenerator.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}
}
