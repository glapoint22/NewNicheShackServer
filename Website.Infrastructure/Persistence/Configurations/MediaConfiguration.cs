using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Thumbnail)
                .HasMaxLength(256);

            builder.Property(x => x.ImageSm)
                .HasMaxLength(256);

            builder.Property(x => x.ImageMd)
                .HasMaxLength(256);

            builder.Property(x => x.ImageLg)
                .HasMaxLength(256);

            builder.Property(x => x.ImageAnySize)
                .HasMaxLength(256);

            builder.Property(x => x.VideoId)
                .HasMaxLength(256);
        }
    }
}