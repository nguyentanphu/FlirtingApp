using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Users.Queries.GetUsers
{
	public class GetUsersQueryResponse
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
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
