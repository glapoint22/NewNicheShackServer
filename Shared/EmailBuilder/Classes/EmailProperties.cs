namespace Shared.EmailBuilder.Classes
{
    public sealed class EmailProperties
    {
        public Recipient Recipient { get; set; } = null!;
        public string Host { get; set; } = string.Empty; 
        public string Link { get; set; } = string.Empty;
        public Person Person { get; set; } = null!;
        public string Var1 { get; set; } = string.Empty;
        public string Var2 { get; set; } = string.Empty;
        public string Stars { get; set; } = string.Empty;
        public EmailProduct Product { get; set; } = null!;

        public string SetEmailBody(string emailBody)
        {
            return emailBody
                .Replace("{host}", Host)
                .Replace("{recipientFirstName}", Recipient.FirstName)
                .Replace("{recipientLastName}", Recipient.LastName)
                .Replace("{link}", Link)
                .Replace("{var1}", Var1)
                .Replace("{var2}", Var2)
                .Replace("{productName}", Product != null ? Product.Name : null)
                .Replace("{productImage}", Product != null ? Product.Image : null)
                .Replace("{productUrl}", Product != null ? Product.Url : null)
                .Replace("{personFirstName}", Person != null ? Person.FirstName : null)
                .Replace("{personLastName}", Person != null ? Person.LastName : null)
                .Replace("{stars}", Stars);
        }
    }
}