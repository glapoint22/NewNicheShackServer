using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserReceivedNoncompliantStrikeListEvent(string UserId, string ListName, string? ListDescription) : INotification;
}