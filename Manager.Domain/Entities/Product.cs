using Shared.Common.Interfaces;
using Shared.Common.ValueObjects;

namespace Manager.Domain.Entities
{
    public sealed class Product : IProduct
    {
        public string Id { get; set; } = string.Empty;
        public Guid? VendorId { get; set; }
        public string SubnicheId { get; set; } = string.Empty;
        public Guid? ImageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Hoplink { get; set; }
        public double Rating { get; set; }
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Disabled { get; set; }

        public Vendor Vendor { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
        public Media Media { get; set; } = null!;


        public ICollection<ProductPrice> ProductPrices { get; private set; } = new HashSet<ProductPrice>();
        public ICollection<ProductInProductGroup> ProductsInProductGroup { get; private set; } = new HashSet<ProductInProductGroup>();
        public ICollection<KeywordGroupBelongingToProduct> KeywordGroupsBelongingToProduct { get; private set; } = new HashSet<KeywordGroupBelongingToProduct>();
    }
}