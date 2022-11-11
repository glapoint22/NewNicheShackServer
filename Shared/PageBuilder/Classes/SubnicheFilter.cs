using Shared.Common.Entities;

namespace Shared.PageBuilder.Classes
{
    public sealed class SubnicheFilter
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UrlName { get; set; } = null!;
        public bool Visible { get; set; } = false;

        public SubnicheFilter(Subniche subniche)
        {
            Id = subniche.Id;
            Name = subniche.Name;
            UrlName = subniche.UrlName;
        }
    }
}