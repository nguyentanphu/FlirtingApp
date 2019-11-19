using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlirtingApp.Infrastructure.Identity.Configurations
{
	class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.Metadata.FindNavigation(nameof(AppUser.RefreshTokens))
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
