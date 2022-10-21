using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductRemovedFromListEvent(string ListId, int ProductId, string UserId) : INotification;
}