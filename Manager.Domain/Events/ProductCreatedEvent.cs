using MediatR;

namespace Manager.Domain.Events
{
    public sealed record ProductCreatedEvent(string Name, Guid ProductId, string UserId) : INotification;
}