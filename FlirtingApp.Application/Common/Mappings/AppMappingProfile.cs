using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces;

namespace FlirtingApp.Application.Common.Mappings
{
	public class AppMappingProfile: Profile
	{
		public AppMappingProfile()
		{
			ApplyMappingFromAssembly(GetType().Assembly);
		}

		private void ApplyMappingFromAssembly(Assembly assembly)
		{
			var publicIMapFromTypes = assembly.GetExportedTypes()
				.Where(t => t.GetInterfaces()
					.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)));

			foreach (var type in publicIMapFromTypes)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");
				methodInfo?.Invoke(instance, new object[] {this});
			}
		}
	}
}
