using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserReceivedNoncompliantStrikeUserNameEvent(string FirstName, string LastName, string Email) : INotification;
}