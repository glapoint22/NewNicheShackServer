using Shared.Common.Classes;
using Shared.Common.Interfaces;
using Shared.Common.ValueObjects;

namespace Website.Domain.Entities
{
    public sealed class Product : Entity, IProduct
    {
        public Guid Id { get; set; }
        public Guid SubnicheId { get; set; }
        public Guid ImageId { get; set; }
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
        public string TrackingCode { get; set; } = string.Empty;


        public Subniche Subniche { get; set; } = null!;
        public Media Media { get; set; } = null!;
        public ICollection<ProductPrice> ProductPrices { get; private set; } = new HashSet<ProductPrice>();
        public ICollection<ProductFilter> ProductFilters { get; private set; } = new HashSet<ProductFilter>();
        public ICollection<ProductKeyword> ProductKeywords { get; private set; } = new HashSet<ProductKeyword>();
        public ICollection<ProductOrder> ProductOrders { get; private set; } = new HashSet<ProductOrder>();
        public ICollection<ProductMedia> ProductMedia { get; private set; } = new HashSet<ProductMedia>();
        public ICollection<ProductInProductGroup> ProductsInProductGroup { get; private set; } = new HashSet<ProductInProductGroup>();
        public ICollection<Notification> Notifications { get; private set; } = new HashSet<Notification>();



        // ---------------------------------------------------------------------- Get Hoplink --------------------------------------------------------------------------
        public string GetHoplink(string? userTrackingCode)
        {
            return Hoplink + (userTrackingCode != null ? (Hoplink.Contains('?') ? "&" : "?") +
                "tid=" + TrackingCode + "_" + userTrackingCode : "");
        }


        // ---------------------------------------------------------------------- Set Rating ---------------------------------------------------------------------------
        public void SetRating(int rating)
        {
            // Increment the star based on the rating. So if the rating is 3, the threeStars property will be incremented
            switch (rating)
            {
                case 1:
                    OneStar++;
                    break;

                case 2:
                    TwoStars++;
                    break;

                case 3:
                    ThreeStars++;
                    break;

                case 4:
                    FourStars++;
                    break;

                case 5:
                    FiveStars++;
                    break;
            }


            // Increment total reviews
            TotalReviews++;

            // Calculate the product's rating
            double sum = 5 * FiveStars +
                         4 * FourStars +
                         3 * ThreeStars +
                         2 * TwoStars +
                         1 * OneStar;

            Rating = Math.Round(sum / TotalReviews, 1);
        }
    }
}