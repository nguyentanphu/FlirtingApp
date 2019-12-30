using System;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Domain.Entities
{
	public class Photo: AuditableEntity
	{
		public string Url { get; set; }
		public string Description { get; set; }
		public bool IsMain { get; set; }
		public Guid UserId { get; set; }
		public string ExternalId { get; set; }
	}
}
