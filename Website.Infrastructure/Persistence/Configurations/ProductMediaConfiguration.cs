﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductMediaConfiguration : IEntityTypeConfiguration<ProductMedia>
    {
        public void Configure(EntityTypeBuilder<ProductMedia> builder)
        {
            builder.HasOne(x => x.Media)
                .WithMany(x => x.ProductMedia)
                .HasForeignKey(x => x.MediaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}