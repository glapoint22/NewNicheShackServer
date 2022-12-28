using MediatR;

namespace Website.Domain.Events
{
    public sealed record PostedReviewEvent(string UserId, Guid ProductId, Guid ReviewId, int Rating, string Title, string Text) : INotification;
}