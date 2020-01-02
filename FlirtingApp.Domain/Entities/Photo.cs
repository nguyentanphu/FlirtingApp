using System;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Domain.Entities
{
	public class Photo: AuditableEntity
	{
		private Photo() { }

		public Photo(
			string url,
			string externalId,
			string description
			)
		{
			Url = url;
			ExternalId = externalId;
			Description = description;
		}
		public string ExternalId { get; private set; }
		public string Url { get; private set; }
		public string Description { get; private set; }
		public bool IsMain { get; private set; }

		public void SetMain()
		{
			IsMain = true;
		}
		public Guid UserId { get; private set; }
		
	}
}
