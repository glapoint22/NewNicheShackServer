namespace Manager.Domain.Entities
{
    public sealed class Keyword
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductKeyword> ProductKeywords { get; private set; } = new HashSet<ProductKeyword>();
        public ICollection<KeywordInKeywordGroup> KeywordsInKeywordGroup { get; private set; } = new HashSet<KeywordInKeywordGroup>();

        public static Keyword Create(string name)
        {
            Keyword keyword = new()
            {
                Name = name.Trim().ToLower()
            };

            return keyword;
        }

        public void UpdateName(string name)
        {
            Name = name.Trim().ToLower();
        }
    }
}
