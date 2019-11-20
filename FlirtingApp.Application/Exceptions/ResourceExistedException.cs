using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class ResourceExistedException : Exception
	{
		public ResourceExistedException(string message = "Resource existed", Exception innerException = null): base(message, innerException)
		{
			
		}

		public static ResourceExistedException FromName(string name, Exception innerException = null)
		{
			var message = $"Resource {name} is existed in the system";
			return new ResourceExistedException(message, innerException);
		}
	}
}
