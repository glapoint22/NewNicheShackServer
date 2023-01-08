using Shared.Common.Classes;

namespace Manager.Domain.Entities
{
    public sealed class ProductFilter : Entity
    {
        public Guid ProductId { get; set; }
        public Guid FilterOptionId { get; set; }

        public FilterOption FilterOption { get; set; } = null!;
        public Product Product { get; set; } = null!;

        public static ProductFilter Create(Guid productId, Guid filterOptionId)
        {
            ProductFilter productFilter = new()
            {
                ProductId = productId,
                FilterOptionId = filterOptionId
            };

            return productFilter;
        }
    }
}