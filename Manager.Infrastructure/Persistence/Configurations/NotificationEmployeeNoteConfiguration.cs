using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class NotificationEmployeeNoteConfiguration : IEntityTypeConfiguration<NotificationEmployeeNote>
    {
        public void Configure(EntityTypeBuilder<NotificationEmployeeNote> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.NotificationEmployeeNotes)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}