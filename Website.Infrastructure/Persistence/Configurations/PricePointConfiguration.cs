using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class PricePointConfiguration : IEntityTypeConfiguration<PricePoint>
    {
        public void Configure(EntityTypeBuilder<PricePoint> builder)
        {
            builder.HasOne(x => x.Media)
                .WithMany(x => x.PricePoints)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductPrice)
                .WithMany(x => x.PricePoints)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Header)
                .HasMaxLength(50);

            builder.Property(x => x.Quantity)
                .HasMaxLength(50);

            builder.Property(x => x.UnitPrice)
                .HasMaxLength(10);

            builder.Property(x => x.Unit)
                .HasMaxLength(25);

            builder.Property(x => x.StrikethroughPrice)
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