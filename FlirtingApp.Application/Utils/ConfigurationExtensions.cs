using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FlirtingApp.Application.Utils
{
	public static class ConfigurationExtensions
	{
		public static TModel GetOptions<TModel>(this IConfiguration config, string sectionName) where TModel : new()
		{
			var model = new TModel();
			config.GetSection(sectionName).Bind(model);
			return model;
		}
	}
}
