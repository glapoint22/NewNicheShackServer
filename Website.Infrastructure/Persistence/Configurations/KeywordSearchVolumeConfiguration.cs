using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class KeywordSearchVolumeConfiguration : IEntityTypeConfiguration<KeywordSearchVolume>
    {
        public void Configure(EntityTypeBuilder<KeywordSearchVolume> builder)
        {
            builder.HasKey(x => new { x.KeywordId, x.Date });
        }
    }
}