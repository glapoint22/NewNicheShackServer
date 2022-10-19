using MediatR;

namespace Website.Domain.Events
{
    public record CollaboratorUpdatedEvent(string CollaboratorUserId, string ListId, bool IsRemoved = false) : INotification;
}