namespace Website.Application.Lists.UpdateCollaborators.Classes
{
    public record ListPermissions(
        bool CanAddToList,
        bool CanShareList,
        bool CanEditList,
        bool CanInviteCollaborators,
        bool CanDeleteList,
        bool CanMoveItem,
        bool CanRemoveItem,
        bool CanManageCollaborators
    );
}