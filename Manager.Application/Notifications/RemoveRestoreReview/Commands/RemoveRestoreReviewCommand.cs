using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RemoveRestoreReview.Commands
{
    public sealed record RemoveRestoreReviewCommand(string UserId, Guid ReviewId, bool AddStrike, bool IsNew, Guid NotificationGroupId) : IRequest<Result>;
}