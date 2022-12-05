using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class PageSubnicheConfiguration : IEntityTypeConfiguration<PageSubniche>
    {
        public void Configure(EntityTypeBuilder<PageSubniche> builder)
        {
            builder.HasKey(x => new { x.PageId, x.SubnicheId });
        }
    }
}