namespace Website.Domain.Entities
{
    public class KeywordSearchVolume
    {
        public int KeywordId { get; set; }
        public DateTime Date { get; set; }

        public Keyword Keyword { get; set; } = null!;
    }
}
