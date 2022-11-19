namespace Website.Domain.Entities
{
    public sealed class PageSubniche
    {
        public int Id { get; set; }
        public string PageId { get; set; } = string.Empty;
        public string SubnicheId { get; set; } = string.Empty;

        public Page Page { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
    }
}