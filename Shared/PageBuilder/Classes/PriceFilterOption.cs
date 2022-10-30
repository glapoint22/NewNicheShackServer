namespace Shared.PageBuilder.Classes
{
    public sealed class PriceFilterOption
    {
        public string Label { get; set; } = string.Empty;
        public double Min { get; set; }
        public double Max { get; set; }
    }
}