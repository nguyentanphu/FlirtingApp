using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FlirtingApp.Application.Photos.Commands.CreatePhoto
{
	public class CreatePhotoRequest
	{
		public IFormFile File { get; set; }
		public string Description { get; set; }
	}
}
