using Shared.Common.Classes;

namespace Shared.PageBuilder.Classes
{
    public sealed class QueriedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string UrlId { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int TotalReviews { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public Image Image { get; set; } = null!;
        public int OneStar { get; set; }
        public int TwoStars { get; set; }
        public int ThreeStars { get; set; }
        public int FourStars { get; set; }
        public int FiveStars { get; set; }
    }
}