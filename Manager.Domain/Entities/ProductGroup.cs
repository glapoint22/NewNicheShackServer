namespace Manager.Domain.Entities
{
    public sealed class ProductGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}