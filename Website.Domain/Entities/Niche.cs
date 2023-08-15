namespace Website.Domain.Entities
{
    public sealed class Niche
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public bool Disabled { get; set; }


        private readonly List<Subniche> _subniches = new();
        public IReadOnlyList<Subniche> Subniches => _subniches.AsReadOnly();
    }
}