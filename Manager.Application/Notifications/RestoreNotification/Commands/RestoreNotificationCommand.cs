using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RestoreNotification.Commands
{
    public sealed record RestoreNotificationCommand(Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}