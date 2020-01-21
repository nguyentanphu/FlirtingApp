using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.Controllers
{
	public class HomeController: ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public HomeController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public IActionResult Index()
		{
			return PhysicalFile(Path.Combine(_environment.WebRootPath, "index.html"), "text/HTML");
		}
	}
}
