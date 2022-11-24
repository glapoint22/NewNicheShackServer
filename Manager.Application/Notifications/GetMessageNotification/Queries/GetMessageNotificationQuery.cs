using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetMessageNotification.Queries
{
    public sealed record GetMessageNotificationQuery(Guid NotificationGroupId, bool IsNew) : IRequest<Result>;
}