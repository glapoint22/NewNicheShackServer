namespace Shared.Common.Entities
{
    public sealed class ProductPrice
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public double Price { get; set; }

        public Product Product { get; set; } = null!;
        public ICollection<PricePoint> PricePoints { get; set; } = new HashSet<PricePoint>();
    }
}