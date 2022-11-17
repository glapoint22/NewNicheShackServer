using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class KeywordGroupConfiguration : IEntityTypeConfiguration<KeywordGroup>
    {
        public void Configure(EntityTypeBuilder<KeywordGroup> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}