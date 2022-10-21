namespace Website.Domain.Entities
{
    public sealed class Subproduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public double Value { get; set; }
        public int Type { get; set; }

        public Media Media { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}