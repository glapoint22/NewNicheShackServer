using MediatR;

namespace Website.Domain.Events
{
    public record UserDeletedEvent(string UserId) : INotification;
}