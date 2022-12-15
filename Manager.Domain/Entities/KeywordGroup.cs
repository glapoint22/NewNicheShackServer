namespace Manager.Domain.Entities
{
    public sealed class KeywordGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool ForProduct { get; set; }

        public ICollection<KeywordInKeywordGroup> KeywordsInKeywordGroup { get; private set; } = new HashSet<KeywordInKeywordGroup>();
        public ICollection<KeywordGroupBelongingToProduct> KeywordGroupsBelongingToProduct { get; private set; } = new HashSet<KeywordGroupBelongingToProduct>();

        public static KeywordGroup Create(string name, bool forProduct = false)
        {
            KeywordGroup keywordGroup = new()
            {
                Name = name,
                ForProduct = forProduct
            };

            return keywordGroup;
        }

        public void EditName(string name)
        {
            Name = name;
        }
    }
}