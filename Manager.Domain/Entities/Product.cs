using Shared.Common.ValueObjects;

namespace Manager.Domain.Entities
{
    public sealed class Product
    {
        public string Id { get; set; } = string.Empty;
        public int VendorId { get; set; }
        public string SubnicheId { get; set; } = string.Empty;
        public Guid ImageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Hoplink { get; set; } = string.Empty;
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Disabled { get; set; }

        public Vendor Vendor { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}