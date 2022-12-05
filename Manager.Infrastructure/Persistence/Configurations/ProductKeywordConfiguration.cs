using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class ProductKeywordConfiguration : IEntityTypeConfiguration<ProductKeyword>
    {
        public void Configure(EntityTypeBuilder<ProductKeyword> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.KeywordId });
        }
    }
}