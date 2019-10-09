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
	public class ApiDbContext: IdentityDbContext<User, Role, Guid>
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
		{
			
		}

		public DbSet<Value> Values { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<User>(ConfigureUser);
			builder.Entity<RefreshToken>(ConfigureRefreshToken);
		}

		public void ConfigureUser(EntityTypeBuilder<User> builder)
		{
			builder.Metadata
				.FindNavigation(nameof(User.RefreshTokens))
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		public void ConfigureRefreshToken(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.HasOne(t => t.User)
				.WithMany(u => u.RefreshTokens)
				.HasForeignKey(t => t.UserId);
		}
	}
}
