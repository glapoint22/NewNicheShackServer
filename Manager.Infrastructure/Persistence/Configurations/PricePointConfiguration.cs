﻿using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class PricePointConfiguration : IEntityTypeConfiguration<PricePoint>
    {
        public void Configure(EntityTypeBuilder<PricePoint> builder)
        {
            builder.HasOne(x => x.Media)
                .WithMany(x => x.PricePoints)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductPrice)
                .WithMany(x => x.PricePoints) 
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Header)
                .HasMaxLength(50);

            builder.Property(x => x.Subheader)
                .HasMaxLength(50);

            builder.Property(x => x.Quantity)
                .HasMaxLength(50);

            builder.Property(x => x.ShippingValue)
                .HasMaxLength(10);
        }
    }
}