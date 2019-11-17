using System;
using System.Collections.Generic;

namespace FlirtingApp.Web.Dtos
{
	public class UserDetail
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string PhotoUrl { get; set; }
		public ICollection<PhotoDto> Photos { get; set; }
	}
}
