using Website.Domain.ValueObjects;

namespace Website.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int SubnicheId { get; set; }
        public int? ImageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Hoplink { get; set; } = string.Empty;
        public int TotalReviews { get; set; }
        public double Rating { get; set; }
        public int OneStar { get; set; }
        public int TwoStars { get; set; }
        public int ThreeStars { get; set; }
        public int FourStars { get; set; }
        public int FiveStars { get; set; }
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Disabled { get; set; }


        public Subniche Subniche { get; set; } = null!;
        public Media Media { get; set; } = null!;
        public ICollection<ProductPrice> ProductPrices { get; private set; } = new HashSet<ProductPrice>();
    }
}