namespace Website.Application.Lists.UpdateCollaborators.Classes
{
    public sealed record ListPermissions
    {
        public bool CanAddToList { get; init; }
        public bool CanShareList { get; init; }
        public bool CanEditList { get; init; }
        public bool CanInviteCollaborators { get; init; }
        public bool CanDeleteList { get; init; }
        public bool CanRemoveFromList { get; init; }
        public bool CanManageCollaborators { get; init; }
    }
}