using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class FilterOptionConfiguration : IEntityTypeConfiguration<FilterOption>
    {
        public void Configure(EntityTypeBuilder<FilterOption> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}