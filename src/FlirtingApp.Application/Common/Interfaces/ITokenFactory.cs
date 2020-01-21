using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface ITokenFactory
	{
		string GenerateToken(int size = 32);
	}
}
