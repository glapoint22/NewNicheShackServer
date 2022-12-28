namespace Website.Domain.Entities
{
    public sealed class ProductMedia
    {
        public Guid ProductId { get; set; }
        public Guid MediaId { get; set; }
        public int Index { get; set; }

        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}