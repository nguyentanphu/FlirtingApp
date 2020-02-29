using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Domain.Common
{
	public abstract class Entity
	{
		public Guid Id { get; protected set; } = Guid.NewGuid();

		public override bool Equals(object obj)
		{
			var other = obj as Entity;
			if (ReferenceEquals(other, null))
			{
				return false;
			}

			if (Id == default || other.Id == default)
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Id == other.Id;
		}

		public static bool operator ==(Entity? a, Entity? b)
		{
			if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
			{
				return true;
			}

			if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
			{
				return false;
			}

			return a.Equals(b);
		}

		public static bool operator !=(Entity? a, Entity? b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
