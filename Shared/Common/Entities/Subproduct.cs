namespace Shared.Common.Entities
{
    public sealed class Subproduct
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int ImageId { get; set; }
        public double Value { get; set; }
        public int Type { get; set; }

        public Media Media { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}