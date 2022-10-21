using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductAddedToListEvent(Guid CollaboratorProductId) : INotification;
}