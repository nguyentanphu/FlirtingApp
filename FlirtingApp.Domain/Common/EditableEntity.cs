using System;

namespace FlirtingApp.Domain.Common
{
	public class AuditableEntity
	{
		public Guid CreatedBy { get; set; }

		public DateTime Created { get; set; }

		public Guid? LastModifiedBy { get; set; }

		public DateTime? LastModified { get; set; }
	}
}
