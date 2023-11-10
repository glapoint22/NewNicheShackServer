using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(3);

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

            builder.HasIndex(x => x.TrackingCode)
                .IncludeProperties(x => x.Id)
                .IsClustered(false);


            builder.HasIndex(x => new
            {
                x.SubnicheId,
                x.Disabled
            }).IncludeProperties(x => new
            {
                x.Id,
                x.ImageId,
                x.Name,
                x.UrlName,
                x.Description,
                x.Hoplink,
                x.TotalReviews,
                x.Rating,
                x.OneStar,
                x.TwoStars,
                x.ThreeStars,
                x.FourStars,
                x.FiveStars,
                x.ShippingType,
                x.Date,
                x.TrackingCode,
                x.Currency
            }).IsClustered(false);




            builder.HasIndex(x => x.Disabled)
                .IncludeProperties(x => x.SubnicheId)
                .IsClustered(false);
        }
    }
}