using MediatR;

namespace Website.Domain.Events
{
    public sealed record ListEditedEvent(string UserId, string PreviousListName, string? PreviousDescription, string CurrentListName, string? CurrentDescription) : INotification;
}