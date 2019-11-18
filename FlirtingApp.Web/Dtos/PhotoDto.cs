using System;

namespace FlirtingApp.Web.Dtos
{
	public class PhotoDto
	{
		public Guid Id { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsMain { get; set; }
	}
}
