using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RestoreAllNotifications.Commands
{
    public sealed record RestoreAllNotificationsCommand(Guid NotificationGroupId) : IRequest<Result>;
}