using MediatR;

namespace Website.Domain.Events
{
    public record ListEvent(string UserId, string ListId, string Name, string? Description) : INotification;
}