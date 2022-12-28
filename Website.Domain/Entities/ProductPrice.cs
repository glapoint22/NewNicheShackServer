namespace Website.Domain.Entities
{
    public sealed class ProductPrice
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }

        public Product Product { get; set; } = null!;
        public ICollection<PricePoint> PricePoints { get; set; } = new HashSet<PricePoint>();
    }
}