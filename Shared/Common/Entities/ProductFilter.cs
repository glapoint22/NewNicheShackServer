namespace Shared.Common.Entities
{
    public sealed class ProductFilter
    {
        public string ProductId { get; set; } = string.Empty;
        public int FilterOptionId { get; set; }

        public FilterOption FilterOption { get; set; } = null!;
    }
}