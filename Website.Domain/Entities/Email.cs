using Shared.EmailBuilder.Classes;

namespace Website.Domain.Entities
{
    public sealed class Email
    {
        public Guid Id { get; set; }
        public EmailType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}