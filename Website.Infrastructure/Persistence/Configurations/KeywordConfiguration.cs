using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IncludeProperties(x => x.Id)
                .IsClustered(false);
        }
    }
}