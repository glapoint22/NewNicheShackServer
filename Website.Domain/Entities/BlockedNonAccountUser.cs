namespace Website.Domain.Entities
{
    public sealed class BlockedNonAccountUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}