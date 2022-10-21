using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserChangedProfileImageEvent(string UserId) : INotification;
}