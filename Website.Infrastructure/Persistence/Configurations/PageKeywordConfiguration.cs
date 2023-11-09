using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class PageKeywordConfiguration : IEntityTypeConfiguration<PageKeyword>
    {
        public void Configure(EntityTypeBuilder<PageKeyword> builder)
        {
            builder.HasKey(x => new { x.PageId, x.KeywordId });

            builder.HasIndex(x => x.KeywordId)
                .IncludeProperties(x => x.PageId)
                .IsClustered(false);
        }
    }
}