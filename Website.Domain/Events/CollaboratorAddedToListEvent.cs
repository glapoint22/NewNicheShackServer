using MediatR;

namespace Website.Domain.Events
{
    public record CollaboratorAddedToListEvent(string ListId, string UserId) : INotification;
}