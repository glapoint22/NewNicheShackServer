using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductFilterConfiguration : IEntityTypeConfiguration<ProductFilter>
    {
        public void Configure(EntityTypeBuilder<ProductFilter> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.FilterOptionId });
        }
    }
}