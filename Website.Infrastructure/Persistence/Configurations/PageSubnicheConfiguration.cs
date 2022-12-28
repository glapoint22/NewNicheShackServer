using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class PageSubnicheConfiguration : IEntityTypeConfiguration<PageSubniche>
    {
        public void Configure(EntityTypeBuilder<PageSubniche> builder)
        {
            builder.HasKey(x => new { x.PageId, x.SubnicheId });
        }
    }
}