using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserActivatedAccountEvent(string UserId) : INotification;
}