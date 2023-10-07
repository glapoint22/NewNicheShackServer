using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Manager.Domain.Entities;

namespace Manager.Infrastructure.Persistence.Configurations
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