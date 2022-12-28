namespace Website.Domain.Entities
{
    public sealed class KeywordSearchVolume
    {
        public Guid KeywordId { get; set; }
        public DateTime Date { get; set; }

        public Keyword Keyword { get; set; } = null!;
    }
}
