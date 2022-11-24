using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RemoveReview.Commands
{
    public sealed record RemoveReviewCommand(Guid ReviewId) : IRequest<Result>;
}