using MediatR;

namespace Manager.Domain.Events
{
    public sealed record PageCreatedEvent(Guid PageId, string UserId) : INotification;
}