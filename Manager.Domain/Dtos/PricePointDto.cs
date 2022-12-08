using Shared.Common.ValueObjects;

namespace Manager.Domain.Dtos
{
    public sealed class PricePointDto
    {
        public Guid Id { get; set; }
        public string? Header { get; set; }
        public string? Quantity { get; set; }
        public Guid? ImageId { get; set; }
        public string? UnitPrice { get; set; }
        public string? Unit { get; set; }
        public string? StrikethroughPrice { get; set; }
        public int ShippingType { get; set; }
        public double Price { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;
    }
}