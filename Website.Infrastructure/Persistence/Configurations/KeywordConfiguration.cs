using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}