using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.Property(x => x.Id)
                .HasMaxLength(21);

            builder.HasIndex(x => x.UserId)
                .IncludeProperties(x => x.Date)
                .IsClustered(false);

            builder.HasIndex(x => new
            {
                x.UserId,
                x.Date
            }).IncludeProperties(x => new
                {
                    x.Id,
                    x.ProductId,
                    x.PaymentMethod,
                    x.Subtotal,
                    x.ShippingHandling,
                    x.Discount,
                    x.Tax,
                    x.Total,
                    x.IsUpsell
                })
                .IsClustered(false);



            builder.HasIndex(x => new
            {
                x.ProductId,
                x.UserId
            }).IsClustered(false);
        }
    }
}