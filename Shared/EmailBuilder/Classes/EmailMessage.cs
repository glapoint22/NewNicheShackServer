namespace Shared.EmailBuilder.Classes
{
    public sealed class EmailMessage
    {
        public string EmailBody { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public EmailProperties EmailProperties { get; set; } = null!;

        public EmailMessage() { }

        public EmailMessage(string emailBody, string emailAddress, string subject, EmailProperties emailProperties)
        {
            EmailBody = emailBody;
            EmailAddress = emailAddress;
            Subject = subject;
            EmailProperties = emailProperties;
            EmailBody = EmailProperties.SetEmailBody(EmailBody);
        }
    }
}