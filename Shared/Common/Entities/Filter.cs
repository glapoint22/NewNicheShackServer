namespace Shared.Common.Entities
{
    public sealed class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<FilterOption> FilterOptions { get; set; } = new HashSet<FilterOption>();
    }
}