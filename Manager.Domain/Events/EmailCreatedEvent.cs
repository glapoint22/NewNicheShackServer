using MediatR;

namespace Manager.Domain.Events
{
    public sealed record EmailCreatedEvent(Guid EmailId, string UserId) : INotification;
}