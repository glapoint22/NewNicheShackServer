using Shared.Common.Classes;

namespace Shared.Common.Entities
{
    public sealed class Collaborator : Entity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ListId { get; set; } = string.Empty;
        public bool IsOwner { get; set; }
        public bool CanAddToList { get; set; }
        public bool CanShareList { get; set; }
        public bool CanEditList { get; set; }
        public bool CanInviteCollaborators { get; set; }
        public bool CanDeleteList { get; set; }
        public bool CanRemoveItem { get; set; }
        public bool CanManageCollaborators { get; set; }

        public User User { get; set; } = null!;
        public List List { get; set; } = null!;
        public ICollection<CollaboratorProduct> CollaboratorProducts { get; private set; } = new HashSet<CollaboratorProduct>();

        public Collaborator() { }

        public Collaborator(string listId, string userId, bool isOwner = false)
        {
            UserId = userId;
            ListId = listId;
            IsOwner = isOwner;
            CanAddToList = isOwner;
            CanShareList = true;
            CanEditList = isOwner;
            CanInviteCollaborators = isOwner;
            CanDeleteList = isOwner;
            CanRemoveItem = isOwner;
            CanManageCollaborators = isOwner;
        }
    }
}
