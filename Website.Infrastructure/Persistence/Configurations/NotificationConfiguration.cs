using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.Name)
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

            builder.HasIndex(x => new
            {
                x.Type,
                x.UserId
            }).IncludeProperties(x => x.NotificationGroupId)
            .IsClustered(false);


            builder.HasIndex(x => x.UserId)
                .IncludeProperties(x => new
                {
                    x.Id,
                    x.ProductId,
                    x.ListId,
                    x.ReviewId,
                    x.Name,
                    x.UserImage,
                    x.Text,
                    x.NonAccountName,
                    x.NonAccountEmail,
                    x.IsArchived,
                    x.CreationDate,
                    x.NotificationGroupId,
                    x.Type
                })
                .IsClustered(false);


            builder.HasIndex(x => x.Type)
                .IncludeProperties(x => new
                {   x.NotificationGroupId,
                    x.Text
                })
                .IsClustered(false);


            builder.HasIndex(x => x.ListId)
                .IncludeProperties(x => new
                {
                    x.Id,
                    x.ProductId,
                    x.UserId,
                    x.ReviewId,
                    x.Name,
                    x.UserImage,
                    x.Text,
                    x.NonAccountName,
                    x.NonAccountEmail,
                    x.IsArchived,
                    x.CreationDate,
                    x.NotificationGroupId,
                    x.Type
                })
                .IsClustered(false);
        }
    }
}