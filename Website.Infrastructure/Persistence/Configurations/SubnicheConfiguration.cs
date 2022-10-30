using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class SubnicheConfiguration : IEntityTypeConfiguration<Subniche>
    {
        public void Configure(EntityTypeBuilder<Subniche> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UrlName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
