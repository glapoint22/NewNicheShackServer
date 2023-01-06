using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.ArchiveNotification.Commands
{
    public sealed record ArchiveNotificationCommand(Guid NotificationGroupId, Guid NotificationId) : IRequest<Result>;
}