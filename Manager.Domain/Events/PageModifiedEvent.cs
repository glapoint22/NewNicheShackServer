using MediatR;

namespace Manager.Domain.Events
{
    public sealed record PageModifiedEvent(Guid PageId, string UserId) : INotification;
}