using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.UrlName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Hoplink)
                .HasMaxLength(256);

            builder.HasOne(x => x.Media)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ImageId);

            builder.HasOne(x => x.Vendor)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);



            builder.OwnsOne(x => x.RecurringPayment, buildAction =>
            {
                buildAction
                    .Property(p => p.TrialPeriod)
                    .HasColumnName("TrialPeriod");

                buildAction
                    .Property(p => p.RecurringPrice)
                    .HasColumnName("RecurringPrice");

                buildAction
                    .Property(p => p.RebillFrequency)
                    .HasColumnName("RebillFrequency");

                buildAction
                    .Property(p => p.TimeFrameBetweenRebill)
                    .HasColumnName("TimeFrameBetweenRebill");

                buildAction
                    .Property(p => p.SubscriptionDuration)
                    .HasColumnName("SubscriptionDuration");
            });
        }
    }
}
