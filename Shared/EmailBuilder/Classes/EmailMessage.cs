namespace Shared.EmailBuilder.Classes
{
    public sealed class EmailMessage
    {
        public string EmailBody { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public EmailProperties EmailProperties { get; set; } = null!;
        public string? SenderEmailAddress { get; set; }

        public EmailMessage(string emailBody, string emailAddress, string subject, EmailProperties emailProperties, string? senderEmailAddress = null)
        {
            EmailBody = emailBody;
            EmailAddress = emailAddress;
            Subject = subject;
            EmailProperties = emailProperties;
            SenderEmailAddress = senderEmailAddress;
        }
    }
}