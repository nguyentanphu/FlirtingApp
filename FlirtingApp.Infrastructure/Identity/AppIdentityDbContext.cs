﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Infrastructure.Identity
{
	

	class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
		{
		}

		public DbSet<AppUser> AppUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);
		}
	}
}
