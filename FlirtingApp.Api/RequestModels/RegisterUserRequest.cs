using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.RequestModels
{
	public class RegisterUserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required]
		public string UserName { get; set; }
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
