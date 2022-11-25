﻿using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Notifications.PostProductNotification.Commands
{
    public sealed record PostProductNotificationCommand(int Type, string ProductId, string? Text) : IRequest<Result>;
}