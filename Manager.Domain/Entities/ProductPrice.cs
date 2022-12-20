using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class ProductPrice : IProductPrice
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }

        public Product Product { get; set; } = null!;
        public ICollection<PricePoint> PricePoints { get; private set; } = new HashSet<PricePoint>();

        public static ProductPrice Create(Guid productId, double price)
        {
            ProductPrice productPrice = new()
            {
                ProductId = productId,
                Price = price
            };

            return productPrice;
        }
    }
}