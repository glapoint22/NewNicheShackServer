using MediatR;

namespace Website.Domain.Events
{
    public record ProductAddedToListEvent(Guid CollaboratorProductId) : INotification;
}