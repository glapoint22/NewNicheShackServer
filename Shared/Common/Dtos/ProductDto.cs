using Shared.Common.Classes;

namespace Shared.Common.Dtos
{
    public sealed class ProductDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int TotalReviews { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public PageImage Image { get; set; } = null!;
        public int OneStar { get; set; }
        public int TwoStars { get; set; }
        public int ThreeStars { get; set; }
        public int FourStars { get; set; }
        public int FiveStars { get; set; }
    }
}