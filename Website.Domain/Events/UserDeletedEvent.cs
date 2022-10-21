using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserDeletedEvent(string UserId) : INotification;
}