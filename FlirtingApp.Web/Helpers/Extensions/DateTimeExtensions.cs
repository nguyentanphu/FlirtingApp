using System;

namespace FlirtingApp.Web.Helpers.Extensions
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
