using MediatR;

namespace Website.Domain.Events
{
    public record UserChangedEmailEvent(string UserId) : INotification;
}