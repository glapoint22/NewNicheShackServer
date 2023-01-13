using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class BlockedNonAccountEmailConfiguration : IEntityTypeConfiguration<BlockedNonAccountUser>
    {
        public void Configure(EntityTypeBuilder<BlockedNonAccountUser> builder)
        {
            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.Name)
                .HasMaxLength(256);
        }
    }
}