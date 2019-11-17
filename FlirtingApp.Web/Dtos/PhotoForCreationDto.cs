using System;
using Microsoft.AspNetCore.Http;

namespace FlirtingApp.Web.Dtos
{
	public class PhotoForCreationDto
	{
		public string Url { get; set; }
		public IFormFile File { get; set; }
		public string Description { get; set; }	
		public DateTime DateAdded { get; } = DateTime.Now;
		public string PublicId { get; set; }
	}
}
