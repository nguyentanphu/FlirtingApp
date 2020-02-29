using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Persistent.ConfigOptions
{
	class MongoOptions
	{
		public string ConnectionString { get; set; } = default!;
		public string Database { get; set; } = default!;
	}
}
