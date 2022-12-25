using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Notifications.PostReviewComplaintNotification.Commands
{
    public sealed record PostReviewComplaintNotificationCommand(Guid ProductId, Guid ReviewId, string? Text) : IRequest<Result>;
}