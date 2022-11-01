using MediatR;

namespace Website.Domain.Events
{
    public sealed record PostedReviewEvent(string UserId, int ProductId, int Rating, string Title, string Text) : INotification;
}