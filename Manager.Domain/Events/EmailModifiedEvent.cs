using MediatR;

namespace Manager.Domain.Events
{
    public sealed record EmailModifiedEvent(Guid EmailId, string UserId) : INotification;
}