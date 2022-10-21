using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.EditList.EventHandlers
{
    public sealed class ListEditedEventHandler : INotificationHandler<ListEditedEvent>
    {
        public Task Handle(ListEditedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}