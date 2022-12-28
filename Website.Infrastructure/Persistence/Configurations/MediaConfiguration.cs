using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

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

            builder.Ignore(x => x.ThumbnailWidth);
            builder.Ignore(x => x.ThumbnailHeight);
            builder.Ignore(x => x.ImageSmWidth);
            builder.Ignore(x => x.ImageSmHeight);
            builder.Ignore(x => x.ImageMdWidth);
            builder.Ignore(x => x.ImageMdHeight);
            builder.Ignore(x => x.ImageLgWidth);
            builder.Ignore(x => x.ImageLgHeight);
            builder.Ignore(x => x.ImageAnySizeWidth);
            builder.Ignore(x => x.ImageAnySizeHeight);
        }
    }
}