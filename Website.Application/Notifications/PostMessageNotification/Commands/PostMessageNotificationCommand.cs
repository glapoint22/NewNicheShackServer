using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Notifications.PostMessageNotification.Commands
{
    public sealed record PostMessageNotificationCommand(string Text) : IRequest<Result>;
}