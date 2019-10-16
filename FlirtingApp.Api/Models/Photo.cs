using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Identity;

namespace FlirtingApp.Api.Models
{
	public class Photo
	{
		public Guid Id { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsMain { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
