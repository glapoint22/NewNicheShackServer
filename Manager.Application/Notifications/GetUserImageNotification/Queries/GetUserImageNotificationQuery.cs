using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetUserImageNotification.Queries
{
    public sealed record GetUserImageNotificationQuery(Guid NotificationGroupId, bool IsNew) : IRequest<Result>;
}