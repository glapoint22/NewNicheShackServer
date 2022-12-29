using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetReviewNotification.Queries
{
    public sealed record GetReviewNotificationQuery(Guid NotificationGroupId, bool IsNew) : IRequest<Result>;
}