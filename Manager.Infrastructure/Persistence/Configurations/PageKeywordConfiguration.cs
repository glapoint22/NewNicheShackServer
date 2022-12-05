using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class PageKeywordConfiguration : IEntityTypeConfiguration<PageKeyword>
    {
        public void Configure(EntityTypeBuilder<PageKeyword> builder)
        {
            builder.HasKey(x => new { x.PageId, x.KeywordInKeywordGroupId });
        }
    }
}