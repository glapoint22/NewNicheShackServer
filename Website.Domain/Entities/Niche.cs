namespace Website.Domain.Entities
{
    public sealed class Niche
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
    }
}