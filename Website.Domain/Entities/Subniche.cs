namespace Website.Domain.Entities
{
    public class Subniche
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public int NicheId { get; set; }

        public Niche Niche { get; set; } = null!;
    }
}