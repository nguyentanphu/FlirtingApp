using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlirtingApp.Api.Helpers.Extensions
{
	public static class DateTimeExtensions
	{
		public static int CalculateAge(this DateTime birthDay)
		{
			var age = DateTime.Today.Year - birthDay.Year;
			if (birthDay.AddYears(age) > DateTime.Today)
			{
				age--;
			}

			return age;
		}
	}
}
