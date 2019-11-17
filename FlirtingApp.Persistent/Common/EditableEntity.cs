using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Persistent.Common
{
	public class AuditableEntity
	{
		public string CreatedBy { get; set; }

		public DateTime Created { get; set; }

		public string LastModifiedBy { get; set; }

		public DateTime? LastModified { get; set; }
	}
}
