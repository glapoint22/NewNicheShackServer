namespace Shared.Common.Entities
{
    public sealed class ProductMedia
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public int MediaId { get; set; }
        public int Index { get; set; }

        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}