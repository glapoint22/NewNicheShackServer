using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class PriceRangeConfiguration : IEntityTypeConfiguration<PriceRange>
    {
        public void Configure(EntityTypeBuilder<PriceRange> builder)
        {
            builder.Property(x => x.Label)
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}