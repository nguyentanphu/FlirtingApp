using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class ResourceNotFoundException: Exception
	{
		public ResourceNotFoundException(string message = "Resource not found", Exception innerException = null): base(message, innerException)
		{
			
		}

		public static ResourceNotFoundException FromGuid(Guid id, Exception innerException = null)
		{
			var message = $"Resource with id {id} was not found";
			return new ResourceNotFoundException(message, innerException);
		}
	}
}
