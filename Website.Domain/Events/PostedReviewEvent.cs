using MediatR;

namespace Website.Domain.Events
{
    public sealed record PostedReviewEvent(string UserId, Guid ProductId, Guid ReviewId) : INotification;
}