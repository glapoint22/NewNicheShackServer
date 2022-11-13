using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Notifications.PostNonAccountMessageNotification.Commands
{
    public sealed record PostNonAccountMessageNotificationCommand(string Name, string Email, string Text) : IRequest<Result>;
}