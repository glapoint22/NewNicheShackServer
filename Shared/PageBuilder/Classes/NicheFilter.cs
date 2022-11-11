using Shared.Common.Entities;

namespace Shared.PageBuilder.Classes
{
    public sealed class NicheFilter
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UrlName { get; set; } = null!;
        public SubnicheFilters SubnicheFilters { get; set; } = null!;
        public bool Visible { get; set; } = false;

        public NicheFilter(Niche niche)
        {
            Id = niche.Id;
            Name = niche.Name;
            UrlName = niche.UrlName;
            SubnicheFilters = new SubnicheFilters((List<Subniche>)niche.Subniches);
        }
    }
}