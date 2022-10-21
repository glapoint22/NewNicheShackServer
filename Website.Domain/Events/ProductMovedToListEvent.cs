using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductMovedToListEvent(string SourceListId, Guid CollaboratorProductId) : INotification;
}