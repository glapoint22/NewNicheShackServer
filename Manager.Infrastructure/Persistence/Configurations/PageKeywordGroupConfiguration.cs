using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class PageKeywordGroupConfiguration : IEntityTypeConfiguration<PageKeywordGroup>
    {
        public void Configure(EntityTypeBuilder<PageKeywordGroup> builder)
        {
            builder.HasKey(x => new { x.PageId, x.KeywordGroupId });
        }
    }
}