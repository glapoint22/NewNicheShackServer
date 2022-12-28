using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductKeywordConfiguration : IEntityTypeConfiguration<ProductKeyword>
    {
        public void Configure(EntityTypeBuilder<ProductKeyword> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.KeywordId });
        }
    }
}