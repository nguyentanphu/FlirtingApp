using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Common.Requests
{
	public class UpdateUserAdditionalInfoRequest
	{
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}
