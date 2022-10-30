using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class FilterConfiguration : IEntityTypeConfiguration<Filter>
    {
        public void Configure(EntityTypeBuilder<Filter> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
