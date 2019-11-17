using System;

namespace FlirtingApp.Persistent.Entities
{
	public class Photo
	{
		public Guid PhotoId { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsMain { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
