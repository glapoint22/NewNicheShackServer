using MediatR;

namespace Website.Domain.Events
{
    public record ListDeletedEvent(string ListName, string UserThatDeletedTheList, List<string> Collaborators) : INotification;
}