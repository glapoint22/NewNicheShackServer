using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
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
        }
    }
}