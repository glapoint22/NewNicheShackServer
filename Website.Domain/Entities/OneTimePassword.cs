namespace Website.Domain.Entities
{
    public class OneTimePassword
    {
        public int Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int Type { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }

        public User User { get; set; } = null!;
    }
}