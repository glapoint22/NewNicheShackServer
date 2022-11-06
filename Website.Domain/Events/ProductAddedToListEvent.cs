using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductAddedToListEvent(string ListId, string ProductId, string UserId) : INotification;
}