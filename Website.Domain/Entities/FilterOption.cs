namespace Website.Domain.Entities
{
    public sealed class FilterOption
    {
        public int Id { get; set; }
        public int FilterId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Filter Filter { get; set; } = null!;
    }
}