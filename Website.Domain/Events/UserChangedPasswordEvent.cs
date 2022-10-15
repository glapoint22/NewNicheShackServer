using MediatR;

namespace Website.Domain.Events
{
    public record UserChangedPasswordEvent(string UserId) : INotification;
}