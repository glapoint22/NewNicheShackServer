using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.FirstName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasMaxLength(50);

            builder.Property(x => x.EmailOnNameChange)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnEmailChange)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnPasswordChange)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnProfileImageChange)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnNewCollaborator)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnRemovedCollaborator)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnRemovedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnMovedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnAddedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnEditedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnDeletedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnReview)
                .HasDefaultValue(true);

            builder.Property(x => x.TrackingCode)
                .HasMaxLength(10);
        }
    }
}