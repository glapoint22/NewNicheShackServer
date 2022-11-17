namespace Shared.Common.Entities
{
    public sealed class Niche
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;


        private readonly List<Subniche> _subniches = new();
        public IReadOnlyList<Subniche> Subniches => _subniches.AsReadOnly();
    }
}