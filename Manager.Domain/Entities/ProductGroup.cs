namespace Manager.Domain.Entities
{
    public sealed class ProductGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductInProductGroup> ProductsInProductGroup { get; private set; } = new HashSet<ProductInProductGroup>();

        public static ProductGroup Create(string name)
        {
            ProductGroup productGroup = new()
            {
                Name = name
            };

            return productGroup;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}