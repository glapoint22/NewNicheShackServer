using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public class ListProductConfiguration : IEntityTypeConfiguration<CollaboratorProduct>
    {
        public void Configure(EntityTypeBuilder<CollaboratorProduct> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.CollaboratorId });
        }
    }
}