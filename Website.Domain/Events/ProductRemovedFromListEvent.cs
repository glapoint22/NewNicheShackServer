using MediatR;

namespace Website.Domain.Events
{
    public record ProductRemovedFromListEvent(string ListId, int ProductId, string UserId) : INotification;
}