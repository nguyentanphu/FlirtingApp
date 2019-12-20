using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FlirtingApp.Application.Common
{
	public abstract class ResponseBase
	{
		[JsonIgnore]
		public bool Success { get; set; }
		[JsonIgnore]
		public string ErrorMessage { get; set; }
	}
}
