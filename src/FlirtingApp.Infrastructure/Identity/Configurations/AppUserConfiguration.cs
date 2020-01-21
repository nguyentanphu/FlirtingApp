using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlirtingApp.Infrastructure.Identity.Configurations
{
	class AppUserConfiguration: IEntityTypeConfiguration<SecurityUser>
	{
		public void Configure(EntityTypeBuilder<SecurityUser> builder)
		{
			builder.OwnsMany(u => u.RefreshTokens, build =>
			{
				build.Property(u => u.Token).HasMaxLength(200);
				build.Property(u => u.RemoteIpAddress).HasMaxLength(50);
			});
			
			builder.Metadata.FindNavigation(nameof(SecurityUser.RefreshTokens))
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
