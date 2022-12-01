namespace Manager.Domain.Entities
{
    public sealed class ProductFilter
    {
        public string ProductId { get; set; } = string.Empty;
        public Guid FilterOptionId { get; set; }

        public FilterOption FilterOption { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}