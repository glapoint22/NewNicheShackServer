using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductMovedToListEvent(string SourceListId, string DestinationListId, string ProductId, string UserId) : INotification;
}