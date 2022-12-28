using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductMovedToListEvent(string SourceListId, string DestinationListId, Guid ProductId, string UserId) : INotification;
}