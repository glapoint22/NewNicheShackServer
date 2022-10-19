namespace Website.Application.Lists.UpdateCollaborators.Classes
{
    public record UpdatedCollaborator(
        int Id,
        ListPermissions ListPermissions,
        bool IsRemoved
    );
}