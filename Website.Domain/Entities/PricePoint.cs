using Shared.Common.ValueObjects;

namespace Website.Domain.Entities
{
    public sealed class PricePoint
    {
        public Guid Id { get; set; }
        public Guid ProductPriceId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ImageId { get; set; }
        public string? Header { get; set; } = string.Empty;
        public string? Quantity { get; set; } = string.Empty;
        public string? UnitPrice { get; set; } = string.Empty;
        public string? Unit { get; set; } = string.Empty;
        public string? StrikethroughPrice { get; set; } = string.Empty;
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;

        public ProductPrice ProductPrice { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}