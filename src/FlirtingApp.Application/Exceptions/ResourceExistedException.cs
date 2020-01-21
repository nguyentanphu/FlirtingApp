using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class ResourceExistedException : Exception
	{
		public ResourceExistedException(string name, string property, Exception innerException = null): base($"{name} with {property} is existed in the system", innerException)
		{
			
		}
	}
}
