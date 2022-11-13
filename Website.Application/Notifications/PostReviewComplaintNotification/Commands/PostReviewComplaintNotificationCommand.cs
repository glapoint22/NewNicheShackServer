using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Notifications.PostReviewComplaintNotification.Commands
{
    public sealed record PostReviewComplaintNotificationCommand(string ProductId, Guid ReviewId, string Text) : IRequest<Result>;
}