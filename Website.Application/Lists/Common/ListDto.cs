using Website.Application.Common.Classes;
using Website.Application.Lists.UpdateCollaborators.Classes;

namespace Website.Application.Lists.Common
{
    public sealed class ListDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalProducts { get; set; }
        public string CollaborateId { get; set; } = string.Empty;
        public int CollaboratorCount { get; set; }
        public int CollaboratorId { get; set; }
        public ListPermissions ListPermissions { get; set; } = null!;
        public string OwnerName { get; set; } = string.Empty;
        public bool IsOwner { get; set; }
        public Image OwnerProfileImage { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}