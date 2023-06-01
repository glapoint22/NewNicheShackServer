namespace Manager.Domain.Entities
{
    public sealed class FilterOption
    {
        public Guid Id { get; set; }
        public Guid FilterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ParamValue { get; set; }

        public Filter Filter { get; set; } = null!;


        public ICollection<ProductFilter> ProductFilters { get; private set; } = new HashSet<ProductFilter>();

        public static FilterOption Create(Guid filterId, string name)
        {
            FilterOption filterOption = new()
            {
                FilterId = filterId,
                Name = name
            };

            return filterOption;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}