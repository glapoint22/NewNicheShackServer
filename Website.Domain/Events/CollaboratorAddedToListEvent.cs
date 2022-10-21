using MediatR;

namespace Website.Domain.Events
{
    public sealed record CollaboratorAddedToListEvent(string ListId, string UserId) : INotification;
}