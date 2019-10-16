using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Dtos
{
	public class UserForListDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string PhotoUrl { get; set; }
	}
}
