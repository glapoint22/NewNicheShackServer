using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetListNotification.Queries
{
    public sealed record GetListNotificationQuery(Guid NotificationGroupId, bool IsNew) : IRequest<Result>;
}