namespace Website.Domain.Entities
{
    public sealed class PageNiche
    {
        public Guid PageId { get; set; }
        public Guid NicheId { get; set; }

        public Page Page { get; set; } = null!;
        public Niche Niche { get; set; } = null!;
    }
}