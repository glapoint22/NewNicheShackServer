using MediatR;

namespace Website.Domain.Events
{
    public record UserActivatedAccountEvent(string UserId) : INotification;
}