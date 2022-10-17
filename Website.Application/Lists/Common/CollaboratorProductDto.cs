using Website.Application.Common.Classes;

namespace Website.Application.Lists.Common
{
    public record CollaboratorProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public double Rating { get; init; }
        public int TotalReviews { get; init; }
        public double MinPrice { get; init; }
        public double MaxPrice { get; init; }
        public string Date { get; init; } = string.Empty;
        public CollaboratorDto Collaborator { get; init; } = null!;
        public string Hoplink { get; init; } = string.Empty;
        public ImageDto Image { get; init; } = null!;
        public string UrlName { get; init; } = string.Empty;
        public int OneStar { get; init; }
        public int TwoStars { get; init; }
        public int ThreeStars { get; init; }
        public int FourStars { get; init; }
        public int FiveStars { get; init; }
    }
}