using MediatR;

namespace Website.Domain.Events
{
    public sealed record PostedReviewEvent(string UserId, string ProductId, int Rating, string Title, string Text) : INotification;
}