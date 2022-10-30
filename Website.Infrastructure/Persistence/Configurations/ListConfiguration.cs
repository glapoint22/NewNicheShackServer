using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Shared.Common.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            builder.Property(x => x.Id)
                .HasMaxLength(10);

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.CollaborateId)
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}