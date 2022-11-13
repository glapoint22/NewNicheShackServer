using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.Classes
{
    public sealed class ProductReviewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Image ProfileImage { get; set; } = null!;
        public string Date { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string ProductId { get; set; } = string.Empty;
    }
}