using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class PageReferenceItemConfiguration : IEntityTypeConfiguration<PageReferenceItem>
    {
        public void Configure(EntityTypeBuilder<PageReferenceItem> builder)
        {
            builder.HasOne(x => x.Subniche)
                .WithMany(x => x.PageReferenceItems)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.KeywordGroup)
                .WithMany(x => x.PageReferenceItems)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.SubnicheId)
                .IsRequired(false);

            builder.Property(x => x.KeywordGroupId)
                .IsRequired(false);
        }
    }
}