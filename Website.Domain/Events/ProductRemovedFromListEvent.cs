using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductRemovedFromListEvent(string ListId, string ProductId, string UserId) : INotification;
}