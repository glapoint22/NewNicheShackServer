using MediatR;

namespace Website.Domain.Events
{
    public record ListEvent(string UserId, string NewName, string? NewDescription) : INotification;
}