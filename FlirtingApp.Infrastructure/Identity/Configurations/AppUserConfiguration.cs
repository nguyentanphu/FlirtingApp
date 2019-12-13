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
			builder.Metadata.FindNavigation(nameof(SecurityUser.RefreshTokens))
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
