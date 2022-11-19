namespace Website.Application.Common.Classes
{
    public sealed class PriceFilter
    {
        public string Caption { get; set; } = string.Empty;
        public List<PriceFilterOption> Options { get; set; } = new List<PriceFilterOption>();
    }
}