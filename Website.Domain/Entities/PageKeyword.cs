namespace Website.Domain.Entities
{
    public sealed class PageKeyword
    {
        public Guid PageId { get; set; }
        public Guid KeywordId { get; set; }

        public Page Page { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}