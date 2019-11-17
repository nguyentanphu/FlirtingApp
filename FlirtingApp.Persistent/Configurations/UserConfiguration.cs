using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlirtingApp.Persistent.Configurations
{
	class UserConfiguration: IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();
			builder.Property(u => u.FirstName).HasMaxLength(100);
			builder.Property(u => u.LastName).HasMaxLength(100);
			builder.Property(u => u.LastActive).HasMaxLength(100);
			builder.Property(u => u.KnownAs).HasMaxLength(100);
			builder.Property(u => u.City).HasMaxLength(100);
			builder.Property(u => u.Country).HasMaxLength(100);

			builder.Property(u => u.Introduction).HasMaxLength(1000);
			builder.Property(u => u.LookingFor).HasMaxLength(1000);
			builder.Property(u => u.Interests).HasMaxLength(1000);

			builder.Metadata.FindNavigation(nameof(User.Photos))
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
