using MediatR;

namespace Website.Domain.Events
{
    public record UserEvent(string UserId) : INotification;
}