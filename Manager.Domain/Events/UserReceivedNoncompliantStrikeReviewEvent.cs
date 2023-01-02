using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserReceivedNoncompliantStrikeReviewEvent(
        string FirstName,
        string LastName,
        string Email,
        string Title,
        string Text) : INotification;
}