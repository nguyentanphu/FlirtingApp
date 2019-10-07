using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Api.Data
{
	public class ApiContext: IdentityDbContext<AppUser>
	{
		public ApiContext(DbContextOptions<ApiContext> options): base(options)
		{
			
		}

		public DbSet<Value> Values { get; set; }
	}
}
