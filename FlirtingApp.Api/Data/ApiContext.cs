using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Identity;
using FlirtingApp.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlirtingApp.Api.Data
{
	public class ApiContext: IdentityDbContext<User, Role, Guid>
	{
		public ApiContext(DbContextOptions<ApiContext> options): base(options)
		{
			
		}

		public DbSet<Value> Values { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			//builder.Entity<User>(ConfigureUser);
		}

		//public void ConfigureUser(EntityTypeBuilder<User> builder)
		//{
		//	builder.HasMany(u => u.RefreshTokens)
		//		.WithOne()
		//		.HasForeignKey(t => t.UserId)
		//		.Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
		//}
	}
}
