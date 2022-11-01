namespace Shared.Common.Entities
{
    public sealed class Keyword
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<KeywordSearchVolume> KeywordSearchVolumes { get; private set; } = new HashSet<KeywordSearchVolume>();
    }
}
