namespace Website.Domain.Entities
{
    public class Collaborator
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ListId { get; set; } = string.Empty;
        public bool IsOwner { get; set; }
        public bool AddToList { get; set; }
        public bool ShareList { get; set; }
        public bool EditList { get; set; }
        public bool InviteCollaborators { get; set; }
        public bool DeleteList { get; set; }
        public bool MoveItem { get; set; }
        public bool RemoveItem { get; set; }

        public User User { get; set; } = null!;
        public List List { get; set; } = null!;
        public ICollection<CollaboratorProduct> CollaboratorProducts { get; private set; } = new HashSet<CollaboratorProduct>();

        public Collaborator(string listId, string userId, bool isOwner)
        {
            UserId = userId;
            ListId = listId;
            IsOwner = isOwner;
            AddToList = isOwner;
            ShareList = true;
            EditList = isOwner;
            InviteCollaborators = isOwner;
            DeleteList = isOwner;
            MoveItem = isOwner;
            RemoveItem = isOwner;
        }
    }
}
