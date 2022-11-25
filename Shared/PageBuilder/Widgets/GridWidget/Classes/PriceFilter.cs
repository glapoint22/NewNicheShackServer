namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public sealed class PriceFilter
    {
        public string Caption { get; set; } = string.Empty;
        public List<PriceFilterOption> Options { get; set; } = new List<PriceFilterOption>();
    }
}