using Shared.Common.Classes;
using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class ProductInProductGroup : Entity, IProductInProductGroup
    {
        public Guid ProductId { get; set; }
        public Guid ProductGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public ProductGroup ProductGroup { get; set; } = null!;

        public static ProductInProductGroup Create(Guid productId, Guid productGroupId)
        {
            ProductInProductGroup productInProductGroup = new()
            {
                ProductId = productId,
                ProductGroupId = productGroupId
            };

            return productInProductGroup;
        }
    }
}