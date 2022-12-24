namespace Shared.EmailBuilder.Classes
{
    public sealed class EmailMessage
    {
        public string EmailContent { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public EmailProperties EmailProperties { get; set; } = null!;
    }
}