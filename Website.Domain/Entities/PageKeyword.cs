namespace Website.Domain.Entities
{
    public sealed class PageKeyword
    {
        public int Id { get; set; }
        public string PageId { get; set; } = string.Empty;
        public int KeywordId { get; set; }

        public Page Page { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}