using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Notifications.PostNonAccountMessageNotification.Commands
{
    public sealed record PostNonAccountMessageNotificationCommand(string Name, string Email, string Text) : IRequest<Result>;
}