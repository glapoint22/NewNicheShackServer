using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

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
                .HasMaxLength(256)
                .IsRequired(true);


            builder.Property(x => x.LineItemType)
                .HasMaxLength(8);

            builder.Property(x => x.RebillFrequency)
                .HasMaxLength(10);

            builder.HasIndex(x => x.OrderId)
                .IncludeProperties(x => new
                {
                    x.Id,
                    x.Name,
                    x.Quantity,
                    x.Price,
                    x.LineItemType,
                    x.RebillFrequency,
                    x.RebillAmount
                }).IsClustered(false);


            builder.HasIndex(x => x.Name)
                .IncludeProperties(x => new
                {
                    x.OrderId,
                    x.LineItemType
                }).IsClustered(false);
        }
    }
}