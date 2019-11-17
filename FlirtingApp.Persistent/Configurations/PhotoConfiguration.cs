using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Persistent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlirtingApp.Persistent.Configurations
{
	class PhotoConfiguration: IEntityTypeConfiguration<Photo>
	{
		public void Configure(EntityTypeBuilder<Photo> builder)
		{
			builder.Property(p => p.Url).HasMaxLength(150);
			builder.Property(p => p.Description).HasMaxLength(1000);

			builder.HasOne(p => p.User)
				.WithMany(u => u.Photos)
				.HasForeignKey(t => t.UserId);
		}
	}
}
