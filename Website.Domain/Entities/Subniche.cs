using Shared.Common.Interfaces;

namespace Website.Domain.Entities
{
    public sealed class Subniche: ISubniche
    {
        public Guid Id { get; set; }
        public Guid NicheId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public Niche Niche { get; set; } = null!;
    }
}