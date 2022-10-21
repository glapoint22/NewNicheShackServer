namespace Website.Application.Lists.UpdateCollaborators.Classes
{
    public sealed record UpdatedCollaborator(
        int Id,
        ListPermissions ListPermissions,
        bool IsRemoved
    );
}