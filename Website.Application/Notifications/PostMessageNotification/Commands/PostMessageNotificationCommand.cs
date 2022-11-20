using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Notifications.PostMessageNotification.Commands
{
    public sealed record PostMessageNotificationCommand(string Text) : IRequest<Result>;
}