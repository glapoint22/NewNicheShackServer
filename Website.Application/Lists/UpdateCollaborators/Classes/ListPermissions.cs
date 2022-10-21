namespace Website.Application.Lists.UpdateCollaborators.Classes
{
    public sealed record ListPermissions(
        bool CanAddToList,
        bool CanShareList,
        bool CanEditList,
        bool CanInviteCollaborators,
        bool CanDeleteList,
        bool CanRemoveItem,
        bool CanManageCollaborators
    );
}