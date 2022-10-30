namespace Shared.Common.Entities
{
    public sealed class ProductFilter
    {
        public int ProductId { get; set; }
        public int FilterOptionId { get; set; }

        public FilterOption FilterOption { get; set; } = null!;
    }
}