namespace Website.Domain.Entities
{
    public sealed class PricePoint
    {
        public Guid Id { get; set; }
        public Guid ProductPriceId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ImageId { get; set; }
        public string? Header { get; set; } = string.Empty;
        public string? Subheader { get; set; }
        public string? Quantity { get; set; } = string.Empty;
        public int ShippingType { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Info { get; set; }
        public string? ShippingValue { get; set; }

        public ProductPrice ProductPrice { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}