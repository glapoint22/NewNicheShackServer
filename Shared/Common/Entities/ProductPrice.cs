namespace Shared.Common.Entities
{
    public sealed class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }

        public Product Product { get; set; } = null!;
    }
}