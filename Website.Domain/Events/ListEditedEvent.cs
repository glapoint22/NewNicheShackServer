using MediatR;

namespace Website.Domain.Events
{
    public record ListEditedEvent(string UserId, string PreviousListName, string? PreviousDescription, string CurrentListName, string? CurrentDescription) : INotification;
}