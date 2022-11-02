namespace Shared.Common.Entities
{
    public sealed class Notification
    {
        public int Id { get; set; }
        public int NotificationGroupId { get; set; }
        public string? UserId { get; set; } = string.Empty;
        public int? ProductId { get; set; }
        public int? ReviewId { get; set; }
        public int Type { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? UserImage { get; set; } = string.Empty;
        public string? Text { get; set; } = string.Empty;
        public string? NonAccountName { get; set; } = string.Empty;
        public string? NonAccountEmail { get; set; } = string.Empty;
        public bool IsArchived { get; set; }
        public DateTime CreationDate { get; set; }


        public NotificationGroup NotificationGroup { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
        public ProductReview ProductReview { get; set; } = null!;
    }
}