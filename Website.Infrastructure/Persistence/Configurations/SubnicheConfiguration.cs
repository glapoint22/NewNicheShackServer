using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class SubnicheConfiguration : IEntityTypeConfiguration<Subniche>
    {
        public void Configure(EntityTypeBuilder<Subniche> builder)
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
                x.NicheId,
                x.Disabled,
                x.Name
            }).IncludeProperties(x => new
            {
                x.Id,
                x.UrlName
            })
            .IsClustered(false);
        }
    }
}
