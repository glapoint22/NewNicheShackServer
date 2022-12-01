namespace Manager.Domain.Entities
{
    public sealed class ProductMedia
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public Guid? MediaId { get; set; }
        public int Index { get; set; }

        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}