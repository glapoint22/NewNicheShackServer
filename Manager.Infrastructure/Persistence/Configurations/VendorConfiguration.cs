using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.PrimaryFirstName)
                .HasMaxLength(256);

            builder.Property(x => x.PrimaryLastName)
                .HasMaxLength(256);

            builder.Property(x => x.PrimaryEmail)
                .HasMaxLength(256);
        }
    }
}