namespace Manager.Domain.Entities
{
    public sealed class Email
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public static Email Create(string name, string content)
        {
            Email email = new()
            {
                Name = name,
                Content = content
            };

            return email;
        }

        public Email Duplicate()
        {
            Email email = new()
            {
                Name = Name,
                Content = Content
            };

            return email;
        }

        public void Update(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}