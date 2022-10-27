using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
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
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.Media)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ImageId);

            builder.Property(x => x.TrackingCode)
                .HasMaxLength(10);

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
