using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserReceivedNoncompliantStrikeUserImageEvent(string FirstName, string LastName, string Email, string UserImage) : INotification;
}