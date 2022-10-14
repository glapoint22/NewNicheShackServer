using Website.Application.Common.Classes;

namespace Website.Application.Lists.List.Common
{
    public record ListProductDto
    {
        public int Id { get; init; }
        public string ListName { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public double Rating { get; init; }
        public int TotalReviews { get; init; }
        public double MinPrice { get; init; }
        public double MaxPrice { get; init; }
        public string DateAdded { get; init; } = string.Empty;
        public ListProductCollaborator Collaborator { get; init; } = null!;
        public string Hoplink { get; init; } = string.Empty;
        public Img Image { get; init; } = null!;
        public string UrlName { get; init; } = string.Empty;
        public int OneStar { get; init; }
        public int TwoStars { get; init; }
        public int ThreeStars { get; init; }
        public int FourStars { get; init; }
        public int FiveStars { get; init; }
    }
}