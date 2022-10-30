using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.Property(x => x.Id)
                .HasMaxLength(21);
        }
    }
}