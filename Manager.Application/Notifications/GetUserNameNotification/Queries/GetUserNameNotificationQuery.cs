using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetUserNameNotification.Queries
{
    public sealed record GetUserNameNotificationQuery(Guid NotificationGroupId, bool IsNew) : IRequest<Result>;
}