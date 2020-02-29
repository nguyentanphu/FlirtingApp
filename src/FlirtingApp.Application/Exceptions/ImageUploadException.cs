using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Exceptions
{
	public class ImageUploadException: Exception
	{
		public ImageUploadException(string userName, string fileName, string contentByteLength): 
			base("File upload to 3rd party failed")
		{
			UserName = userName;
			FileName = fileName;
			ContentByteLength = contentByteLength;
		}
		public string UserName { get; }
		public string FileName { get; }
		public string ContentByteLength { get; }
	}
}
