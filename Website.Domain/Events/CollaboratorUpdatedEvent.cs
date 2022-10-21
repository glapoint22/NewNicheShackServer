using MediatR;

namespace Website.Domain.Events
{
    public sealed record CollaboratorUpdatedEvent(string CollaboratorUserId, string ListId, bool IsRemoved = false) : INotification;
}