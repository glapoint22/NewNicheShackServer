using MediatR;

namespace Website.Domain.Events
{
    public sealed record CollaboratorRemovedEvent(string UserId, string CollaboratorUserId, string ListId) : INotification;
}