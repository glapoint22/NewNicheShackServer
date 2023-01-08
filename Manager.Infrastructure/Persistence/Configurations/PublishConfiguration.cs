using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    internal class PublishConfiguration : IEntityTypeConfiguration<Publish>
    {
        public void Configure(EntityTypeBuilder<Publish> builder)
        {
            builder.HasOne(x => x.Product)
               .WithMany(x => x.Publishes)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);

            builder.HasOne(x => x.Page)
               .WithMany(x => x.Publishes)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);


            builder.HasOne(x => x.Email)
               .WithMany(x => x.Publishes)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);
        }
    }
}