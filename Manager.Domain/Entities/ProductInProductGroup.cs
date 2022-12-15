using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class ProductInProductGroup : IProductInProductGroup
    {
        public string ProductId { get; set; } = string.Empty;
        public Guid ProductGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public ProductGroup ProductGroup { get; set; } = null!;

        public static ProductInProductGroup Create(string productId, Guid productGroupId)
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