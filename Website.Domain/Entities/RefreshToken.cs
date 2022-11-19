namespace Website.Domain.Entities
{
    public sealed class RefreshToken
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        public User User { get; set; } = null!;
    }
}