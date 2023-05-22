using Website.Application.Common.Classes;

namespace Website.Application.Lists.Common
{
    public sealed class ListProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int TotalReviews { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string DateAdded { get; set; } = string.Empty;
        public CollaboratorDto Collaborator { get; set; } = null!;
        public string Hoplink { get; set; } = string.Empty;
        public Image Image { get; set; } = null!;
        public string UrlName { get; set; } = string.Empty;
        public int OneStar { get; set; }
        public int TwoStars { get; set; }
        public int ThreeStars { get; set; }
        public int FourStars { get; set; }
        public int FiveStars { get; set; }
        public bool Disabled { get; set; }
    }
}