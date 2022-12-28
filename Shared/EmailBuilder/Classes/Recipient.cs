namespace Shared.EmailBuilder.Classes
{
    public sealed class Recipient : Person
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}