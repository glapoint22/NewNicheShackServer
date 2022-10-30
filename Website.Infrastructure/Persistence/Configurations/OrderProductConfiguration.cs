using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasOne(x => x.ProductOrder)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.OrderId)
                .HasMaxLength(21);

            builder.Property(x => x.Name)
                .HasMaxLength(21);

            builder.Property(x => x.LineItemType)
                .HasMaxLength(8);
        }
    }
}