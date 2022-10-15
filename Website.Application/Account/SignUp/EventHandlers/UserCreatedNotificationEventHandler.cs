﻿using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.EventHandlers
{
    public class UserCreatedNotificationEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}