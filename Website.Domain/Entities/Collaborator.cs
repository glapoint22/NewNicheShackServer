using Website.Domain.Classes;

namespace Website.Domain.Entities
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
        public bool CanRemoveFromList { get; set; }
        public bool CanManageCollaborators { get; set; }

        public User User { get; set; } = null!;
        public List List { get; set; } = null!;


        public static Collaborator Create(string listId, string userId, bool isOwner = false)
        {
            return new()
            {
                UserId = userId,
                ListId = listId,
                IsOwner = isOwner,
                CanAddToList = true,
                CanShareList = true,
                CanEditList = isOwner,
                CanInviteCollaborators = isOwner,
                CanDeleteList = isOwner,
                CanRemoveFromList = isOwner,
                CanManageCollaborators = isOwner
            };
        }
    }
}
