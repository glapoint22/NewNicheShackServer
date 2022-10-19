using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.EditList.EventHandlers
{
    public class ListEditedEventHandler : INotificationHandler<ListEditedEvent>
    {
        public Task Handle(ListEditedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}