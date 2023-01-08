using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductInProductGroupConfiguration : IEntityTypeConfiguration<ProductInProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductInProductGroup> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.ProductGroupId });
        }
    }
}