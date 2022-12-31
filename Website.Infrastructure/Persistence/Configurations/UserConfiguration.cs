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

            builder.Property(x => x.EmailOnNameUpdated)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnEmailUpdated)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnPasswordUpdated)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnProfileImageUpdated)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorJoinedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserJoinedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserRemovedFromList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorRemovedFromList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserRemovedCollaborator)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorAddedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserAddedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorRemovedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserRemovedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorMovedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserMovedListItem)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorUpdatedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserUpdatedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnCollaboratorDeletedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnUserDeletedList)
                .HasDefaultValue(true);

            builder.Property(x => x.EmailOnItemReviewed)
                .HasDefaultValue(true);

            builder.Property(x => x.TrackingCode)
                .HasMaxLength(10);
        }
    }
}