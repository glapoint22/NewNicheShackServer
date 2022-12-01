namespace Manager.Domain.Entities
{
    public sealed class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PrimaryEmail { get; set; }
        public string? PrimaryFirstName { get; set; }
        public string? PrimaryLastName { get; set; }

        public ICollection<Product> Products { get; private set; } = new HashSet<Product>();
    }
}