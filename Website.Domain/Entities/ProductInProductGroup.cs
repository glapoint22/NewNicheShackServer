using Shared.Common.Interfaces;

namespace Website.Domain.Entities
{
    public sealed class ProductInProductGroup : IProductInProductGroup
    {
        public Guid ProductId { get; set; }
        public Guid ProductGroupId { get; set; }

        public Product Product { get; set; } = null!;
    }
}