using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserChangedEmailEvent(string UserId) : INotification;
}