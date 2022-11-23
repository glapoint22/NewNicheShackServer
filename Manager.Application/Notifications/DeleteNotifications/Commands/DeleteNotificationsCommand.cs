using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.DeleteNotifications.Commands
{
    public sealed record DeleteNotificationsCommand(Guid NotificationGroupId, List<Guid> NotificationIds) : IRequest<Result>;
}