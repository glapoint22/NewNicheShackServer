namespace Shared.Common.Entities
{
    public sealed class ProductReview
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool Deleted { get; set; }

        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<Notification> Notifications { get; private set; } = new HashSet<Notification>();


        public static ProductReview Create(string productId, string userId, string title, double rating, string text)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                UserId = userId,
                Title = title,
                Rating = rating,
                Date = DateTime.UtcNow,
                Text = text
            };
        }


        public void Rate(int likes, int dislikes)
        {
            // Increment the likes or dislikes
            Likes += likes;
            Dislikes += dislikes;
        }
    }
}