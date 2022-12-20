using Shared.Common.Classes;

namespace Manager.Domain.Entities
{
    public sealed class Subniche
    {
        public Guid Id { get; set; }
        public Guid NicheId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public Niche Niche { get; set; } = null!;

        public static Subniche Create(Guid nicheId, string name)
        {
            Subniche subniche = new()
            {
                NicheId = nicheId,
                Name = name,
                UrlName = Utility.GenerateUrlName(name),
            };

            return subniche;
        }

        public void UpdateName(string name)
        {
            Name = name.Trim();
            UrlName = Utility.GenerateUrlName(name);
        }
    }
}