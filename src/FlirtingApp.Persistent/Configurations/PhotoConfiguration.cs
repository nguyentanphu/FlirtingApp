﻿using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Domain.Entities;
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
			builder.Property(p => p.ExternalId).HasMaxLength(50);
		}
	}
}
