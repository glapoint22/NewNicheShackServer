using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserReceivedNoncompliantStrikeUserImageEvent(string UserId, string UserImage) : INotification;
}