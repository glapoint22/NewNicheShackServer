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

        public static Vendor Create(string name, string? primaryEmail, string? primaryFirstName, string? primaryLastName)
        {
            Vendor vendor = new()
            {
                Name = name,
                PrimaryEmail = primaryEmail,
                PrimaryFirstName = primaryFirstName,
                PrimaryLastName = primaryLastName
            };

            return vendor;
        }

        public void Update(string name, string? primaryEmail, string? primaryFirstName, string? primaryLastName)
        {
            Name = name;
            PrimaryEmail = primaryEmail;
            PrimaryFirstName = primaryFirstName;
            PrimaryLastName = primaryLastName;
        }
    }
}