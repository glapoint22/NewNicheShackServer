using MediatR;

namespace Website.Domain.Events
{
    public record UserChangedProfileImageEvent(string UserId) : INotification;
}