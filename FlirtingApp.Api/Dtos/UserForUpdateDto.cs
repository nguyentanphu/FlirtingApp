using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Dtos
{
	public class UserForUpdateDto
	{
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}
