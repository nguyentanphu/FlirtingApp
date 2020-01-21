using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class EmptyFileException: Exception
	{
		public EmptyFileException(string message = "Cannot upload empty file")
		{
			
		}
	}
}
