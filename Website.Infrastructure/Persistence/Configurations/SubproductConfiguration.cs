using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class SubproductConfiguration : IEntityTypeConfiguration<Subproduct>
    {
        public void Configure(EntityTypeBuilder<Subproduct> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();


            builder.HasOne(x => x.Media)
                .WithMany(x => x.Subproducts)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}