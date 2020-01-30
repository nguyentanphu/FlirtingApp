using Newtonsoft.Json;
using System.Reflection;

namespace FlirtingApp.Infrastructure.MongoMessageBroker
{
	public class ContractResolverWithPrivates : Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver
	{
		protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(
			MemberInfo member, MemberSerialization memberSerialization)
		{
			var prop = base.CreateProperty(member, memberSerialization);

			if (!prop.Writable)
			{
				var property = member as PropertyInfo;
				if (property != null)
				{
					var hasPrivateSetter = property.GetSetMethod(true) != null;
					prop.Writable = hasPrivateSetter;
				}
			}

			return prop;
		}
	}

	public class CustomJsonSerializerSettings
	{
		public static JsonSerializerSettings GetSerializerSettings()
		{
			var settings = new JsonSerializerSettings();
			settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
			settings.ContractResolver = new ContractResolverWithPrivates();

			return settings;
		}
	}
}