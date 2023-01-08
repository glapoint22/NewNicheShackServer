using Shared.Common.Classes;
using Shared.EmailBuilder.Classes;

namespace Manager.Domain.Entities
{
    public sealed class Email : Entity
    {
        public Guid Id { get; set; }
        public EmailType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public ICollection<Publish> Publishes { get; private set; } = new HashSet<Publish>();

        public static Email Create(string name, string content)
        {
            Email email = new()
            {
                Type = EmailType.None,
                Name = name,
                Content = content
            };

            return email;
        }

        public Email Duplicate()
        {
            Email email = new()
            {
                Type = Type,
                Content = Content,
                Name = Name
            };

            return email;
        }

        public void Update(EmailType type, string name, string content)
        {
            Type = type;
            Name = name;
            Content = content;
        }
    }
}