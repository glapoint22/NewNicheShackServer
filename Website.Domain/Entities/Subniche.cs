namespace Website.Domain.Entities
{
    public sealed class Subniche
    {
        public int Id { get; set; }
        public int NicheId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public Niche Niche { get; set; } = null!;
    }
}