using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ListProductConfiguration : IEntityTypeConfiguration<ListProduct>
    {
        public void Configure(EntityTypeBuilder<ListProduct> builder)
        {
            builder.HasKey(x => new { x.ListId, x.ProductId, x.UserId });
        }
    }
}