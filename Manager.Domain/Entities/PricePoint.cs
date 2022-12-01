using Shared.Common.ValueObjects;

namespace Manager.Domain.Entities
{
    public sealed class PricePoint
    {
        public Guid Id { get; set; }
        public Guid ProductPriceId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public Guid? ImageId { get; set; }
        public string? Header { get; set; }
        public string? Quantity { get; set; }
        public string? UnitPrice { get; set; } 
        public string? Unit { get; set; }
        public string? StrikethroughPrice { get; set; }
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;

        public ProductPrice ProductPrice { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}