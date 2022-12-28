using Shared.Common.Dtos;

namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public sealed class NicheFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string UrlName { get; set; } = null!;
        public SubnicheFilters SubnicheFilters { get; set; } = null!;
        public bool Visible { get; set; } = false;

        public NicheFilter(NicheDto niche)
        {
            Id = niche.Id;
            Name = niche.Name;
            UrlName = niche.UrlName;
            SubnicheFilters = new SubnicheFilters(niche.Subniches);
        }
    }
}