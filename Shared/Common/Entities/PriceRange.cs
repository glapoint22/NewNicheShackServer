namespace Shared.Common.Entities
{
    public sealed class PriceRange
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public int Min { get; set; }
        public int Max { get; set; }
    }
}