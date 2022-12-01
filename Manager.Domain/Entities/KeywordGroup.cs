namespace Manager.Domain.Entities
{
    public sealed class KeywordGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool ForProduct { get; set; }
    }
}