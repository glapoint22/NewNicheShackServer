using MediatR;

namespace Website.Domain.Events
{
    public record ProductMovedToListEvent(string SourceListId, Guid CollaboratorProductId) : INotification;
}