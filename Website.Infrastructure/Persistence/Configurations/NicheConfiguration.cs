using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class NicheConfiguration : IEntityTypeConfiguration<Niche>
    {
        public void Configure(EntityTypeBuilder<Niche> builder)
        {
            builder.Property(x => x.Id)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UrlName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}