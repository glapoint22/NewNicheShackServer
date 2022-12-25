using MediatR;

namespace Website.Domain.Events
{
    public sealed record ProductAddedToListEvent(string ListId, Guid ProductId, string UserId) : INotification;
}