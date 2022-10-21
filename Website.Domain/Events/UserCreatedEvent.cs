using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserCreatedEvent(string UserId) : INotification;
}