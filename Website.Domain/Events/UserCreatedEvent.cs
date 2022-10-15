using MediatR;

namespace Website.Domain.Events
{
    public record UserCreatedEvent(string UserId) : INotification;
}