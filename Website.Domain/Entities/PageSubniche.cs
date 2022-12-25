namespace Website.Domain.Entities
{
    public sealed class PageSubniche
    {
        public Guid PageId { get; set; }
        public Guid SubnicheId { get; set; }

        public Page Page { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
    }
}