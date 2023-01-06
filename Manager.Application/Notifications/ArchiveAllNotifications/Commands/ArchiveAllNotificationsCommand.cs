using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.ArchiveAllNotifications.Commands
{
    public sealed record ArchiveAllNotificationsCommand(Guid NotificationGroupId) : IRequest<Result>;
}