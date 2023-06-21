using Shared.Common.ValueObjects;

namespace Manager.Domain.Dtos
{
    public sealed class PricePointDto
    {
        public Guid Id { get; set; }
        public string? Header { get; set; }
        public string? Subheader { get; set; }
        public string? Quantity { get; set; }
        public Guid? ImageId { get; set; }
        public int ShippingType { get; set; }
        public double Price { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Info { get; set; }
        public string? ShippingValue { get; set; }
    }
}