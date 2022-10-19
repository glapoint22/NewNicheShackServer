using MediatR;

namespace Website.Domain.Events
{
    public record ProductAddedToListEvent(string ListId, int ProductId, string UserId) : INotification;
}