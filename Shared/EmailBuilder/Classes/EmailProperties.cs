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
        public string Var3 { get; set; } = string.Empty;
        public string Var4 { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string ImageSrc { get; set; } = string.Empty;
        public string Stars { get; set; } = string.Empty;

        public string SetEmailBody(string emailBody)
        {
            return emailBody
                .Replace("{host}", Host)
                .Replace("{recipientFirstName}", Recipient.FirstName)
                .Replace("{recipientLastName}", Recipient.LastName)
                .Replace("{link}", Link)
                .Replace("{var1}", Var1)
                .Replace("{var2}", Var2)
                .Replace("{var3}", Var3)
                .Replace("{var4}", Var4)
                .Replace("{imageName}", ImageName)
                .Replace("{imageSrc}", ImageSrc)
                .Replace("{personFirstName}", Person?.FirstName)
                .Replace("{personLastName}", Person?.LastName)
                .Replace("{stars}", Stars);
        }
    }
}