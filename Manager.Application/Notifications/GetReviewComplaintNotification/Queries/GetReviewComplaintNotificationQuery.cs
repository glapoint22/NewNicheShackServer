using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.GetReviewComplaintNotification.Queries
{
    public sealed record GetReviewComplaintNotificationQuery(Guid NotificationGroupId) : IRequest<Result>;
}