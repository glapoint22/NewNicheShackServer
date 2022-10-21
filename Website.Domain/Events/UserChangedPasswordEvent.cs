using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserChangedPasswordEvent(string UserId) : INotification;
}