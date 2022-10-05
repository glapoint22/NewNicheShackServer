using Website.Domain.ValueObjects;

namespace Website.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SubnicheId { get; set; }
        public string UrlName { get; set; } = string.Empty;
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

        public Subniche Subniche { get; set; } = null!;
    }
}