using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlirtingApp.Infrastructure.Identity.Configurations
{
	class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.Property(r => r.RemoteIpAddress).HasMaxLength(50);
			builder.Property(r => r.Token).HasMaxLength(200);

			builder.HasOne(r => r.AppUser)
				.WithMany(a => a.RefreshTokens)
				.HasForeignKey(r => r.AppUserId);

		}
	}
}
