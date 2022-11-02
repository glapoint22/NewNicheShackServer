using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.UserName)
                .HasMaxLength(200);

            builder.Property(x => x.UserImage)
                .HasMaxLength(50);

            builder.Property(x => x.NonAccountName)
                .HasMaxLength(256);

            builder.Property(x => x.NonAccountEmail)
                .HasMaxLength(256);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasOne(x => x.ProductReview)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Notifications)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}