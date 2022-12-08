using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class ProductPrice : IProductPrice
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public double Price { get; set; }

        public Product Product { get; set; } = null!;
        public ICollection<PricePoint> PricePoints { get; private set; } = new HashSet<PricePoint>();

        public static ProductPrice Create(string productId, double price)
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