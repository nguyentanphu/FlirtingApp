using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlirtingApp.Api.Configurations
{
	public static class Extensions
	{
		public static void AddApplicationError(this HttpResponse reponse, string message)
		{
			reponse.Headers.Add("Application-Error", message);
			reponse.Headers.Add("Access-Control-Allow-Origin", "*");
		}
	}
}
