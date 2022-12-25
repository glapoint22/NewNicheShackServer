using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductRemovedFromListEvent(string ListId, Guid ProductId, string UserId) : INotification;
}