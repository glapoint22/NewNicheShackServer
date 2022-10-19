﻿using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.UpdateCollaborators.EventHandlers
{
    public class CollaboratorUpdatedEventHandler : INotificationHandler<CollaboratorUpdatedEvent>
    {
        public Task Handle(CollaboratorUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}