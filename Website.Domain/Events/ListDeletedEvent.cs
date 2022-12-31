using MediatR;

namespace Website.Domain.Events
{
    public sealed record ListDeletedEvent(string ListName, string UserId, List<string> Collaborators) : INotification;
}