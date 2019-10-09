using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.RequestModels
{
	public class LoginRequest
	{
		[Required]
		public string UserName { get; set; }
		[StringLength(maximumLength: 10, MinimumLength = 4, ErrorMessage = "Password has to be bet 4 - 8 characters")]
		public string Password { get; set; }
	}
}
