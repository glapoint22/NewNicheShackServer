using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class KeywordGroupBelongingToProductConfiguration : IEntityTypeConfiguration<KeywordGroupBelongingToProduct>
    {
        public void Configure(EntityTypeBuilder<KeywordGroupBelongingToProduct> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.KeywordGroupId });
        }
    }
}