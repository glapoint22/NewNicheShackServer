using MediatR;

namespace Manager.Domain.Events
{
    public sealed record ProductModifiedEvent(Guid ProductId, string UserId) : INotification;
}