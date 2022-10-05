namespace Website.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string UserComment { get; set; } = string.Empty;
        public string NonAccountUserName { get; set; } = string.Empty;
        public string NonAccountUserEmail { get; set; } = string.Empty;
        public int? ReviewId { get; set; }
        public DateTime CreationDate { get; set; }

        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
        public ProductReview ProductReview { get; set; } = null!;
    }
}