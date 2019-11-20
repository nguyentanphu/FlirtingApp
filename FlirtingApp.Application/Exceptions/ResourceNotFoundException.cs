using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class ResourceNotFoundException: Exception
	{
		public ResourceNotFoundException(string name, Guid id, Exception innerException = null): base($"{name} with id {id} was not found", innerException)
		{
			
		}
	}
}
