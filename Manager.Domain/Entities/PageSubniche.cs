using Shared.Common.Classes;

namespace Manager.Domain.Entities
{
    public sealed class PageSubniche : Entity
    {
        public Guid PageId { get; set; }
        public Guid SubnicheId { get; set; }

        public Page Page { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;

        public static PageSubniche Create(Guid pageId, Guid subnicheId)
        {
            PageSubniche pageSubniche = new()
            {
                PageId = pageId,
                SubnicheId = subnicheId
            };

            return pageSubniche;
        }
    }
}