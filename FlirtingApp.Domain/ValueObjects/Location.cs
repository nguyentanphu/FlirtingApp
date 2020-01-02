using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Domain.ValueObjects
{
	public class Location: ValueObject<Location>
	{
		public static Location UnknownLocation = new Location(new []{0d, 0d});
		private const string defaultType = "Point";
		private Location() { }

		public Location(double[] coordinates)
		{
			if (coordinates == null || coordinates.Length != 2)
			{
				throw new ArgumentException("Not valid coordinate", nameof(coordinates));
			}

			Coordinates = coordinates;
		}

		public string Type { get; private set; } = defaultType;
		public double[] Coordinates { get; private set; }
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Type;
			yield return Coordinates;
		}
	}
}
