using MediatR;

namespace Manager.Domain.Events
{
    public sealed record ProductCreatedEvent(Guid ProductId, string UserId) : INotification;
}