using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence.Configurations
{
    public sealed class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.ProductId,
                x.Likes,
                x.Rating,
                x.Deleted
            }).IncludeProperties(x => new
            {
                x.Id,
                x.UserId,
                x.Title,
                x.Date,
                x.Text,
                x.Dislikes
            })
            .IsClustered(false);
        }
    }
}