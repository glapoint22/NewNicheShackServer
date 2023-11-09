using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

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

            builder.HasIndex(x => new
            {
                x.Disabled,
                x.Name
            })
                .IncludeProperties(x => new
                {
                    x.Id,
                    x.UrlName
                })
                .IsClustered(false);
        }
    }
}