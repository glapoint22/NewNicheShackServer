using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class ProductInProductGroup : IProductInProductGroup
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public Guid ProductGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public ProductGroup ProductGroup { get; set; } = null!;
    }
}