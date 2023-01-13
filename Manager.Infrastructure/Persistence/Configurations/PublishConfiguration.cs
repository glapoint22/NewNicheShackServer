using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infrastructure.Persistence.Configurations
{
    internal class PublishConfiguration : IEntityTypeConfiguration<PublishItem>
    {
        public void Configure(EntityTypeBuilder<PublishItem> builder)
        {
            builder.HasOne(x => x.Product)
               .WithMany(x => x.PublishItems)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);

            builder.HasOne(x => x.Page)
               .WithMany(x => x.PublishItems)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);


            builder.HasOne(x => x.Email)
               .WithMany(x => x.PublishItems)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);
        }
    }
}