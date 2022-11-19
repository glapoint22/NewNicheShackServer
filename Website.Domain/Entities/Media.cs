namespace Website.Domain.Entities
{
    public sealed class Media
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Thumbnail { get; set; } = string.Empty;
        public string? ImageSm { get; set; } = string.Empty;
        public string? ImageMd { get; set; } = string.Empty;
        public string? ImageLg { get; set; } = string.Empty;
        public string? ImageAnySize { get; set; } = string.Empty;
        public string? VideoId { get; set; } = string.Empty;
        public int MediaType { get; set; }
        public int VideoType { get; set; }

        public ICollection<Product> Products { get; private set; } = new HashSet<Product>();
        public ICollection<Subproduct> Subproducts { get; private set; } = new HashSet<Subproduct>();
        public ICollection<PricePoint> PricePoints { get; private set; } = new HashSet<PricePoint>();
        public ICollection<ProductMedia> ProductMedia { get; private set; } = new HashSet<ProductMedia>();
    }
}