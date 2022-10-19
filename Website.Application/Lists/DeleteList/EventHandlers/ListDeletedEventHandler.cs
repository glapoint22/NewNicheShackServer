﻿using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.DeleteList.EventHandlers
{
    public class ListDeletedEventHandler : INotificationHandler<ListDeletedEvent>
    {
        public Task Handle(ListDeletedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}