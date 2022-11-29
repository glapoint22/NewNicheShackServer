namespace Manager.Domain.Entities
{
    public sealed class Subniche
    {
        public string Id { get; set; } = string.Empty;
        public string NicheId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public Niche Niche { get; set; } = null!;
    }
}