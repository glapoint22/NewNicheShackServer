using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(x => x.Id)
                .HasMaxLength(256);

            builder.Property(x => x.DeviceId)
                .HasMaxLength(256);


            builder.HasIndex(x => new
            {
                x.Id,
                x.UserId,
                x.DeviceId
            }).IncludeProperties(x => x.Expiration)
            .IsClustered(false);
        }
    }
}