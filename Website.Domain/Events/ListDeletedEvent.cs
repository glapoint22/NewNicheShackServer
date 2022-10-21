using MediatR;

namespace Website.Domain.Events
{
    public sealed record ListDeletedEvent(string ListName, string UserThatDeletedTheList, List<string> Collaborators) : INotification;
}