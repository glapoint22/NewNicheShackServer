using Shared.Common.Classes;

namespace Manager.Domain.Entities
{
    public sealed class Niche
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public static Niche Create(string name)
        {
            Niche niche = new()
            {
                Name = name.Trim(),
                UrlName = Utility.GenerateUrlName(name)
            };

            return niche;
        }

        public void UpdateName(string name)
        {
            Name = name.Trim();
            UrlName = Utility.GenerateUrlName(name);
        }
    }
}