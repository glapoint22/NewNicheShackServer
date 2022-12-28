namespace Website.Domain.Entities
{
    public sealed class ProductFilter
    {
        public Guid ProductId { get; set; }
        public Guid FilterOptionId { get; set; }

        public FilterOption FilterOption { get; set; } = null!;
    }
}