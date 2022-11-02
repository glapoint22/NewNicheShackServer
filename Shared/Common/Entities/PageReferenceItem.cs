namespace Shared.Common.Entities
{
    public sealed class PageReferenceItem
    {
        public int Id { get; set; }
        public string PageId { get; set; } = string.Empty;
        public string? SubnicheId { get; set; }
        public int? KeywordGroupId { get; set; }

        public Page Page { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;
    }
}