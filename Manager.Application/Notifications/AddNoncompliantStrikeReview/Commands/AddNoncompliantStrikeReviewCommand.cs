using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.AddNoncompliantStrikeReview.Commands
{
    public sealed record AddNoncompliantStrikeReviewCommand(string UserId, Guid ReviewId, bool AddStrike) : IRequest<Result>;
}