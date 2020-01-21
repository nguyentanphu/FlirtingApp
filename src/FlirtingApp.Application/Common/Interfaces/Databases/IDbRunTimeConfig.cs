using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces.Databases
{
	public interface IDbRunTimeConfig
	{
		Task CreateLocationIndex();
	}
}
