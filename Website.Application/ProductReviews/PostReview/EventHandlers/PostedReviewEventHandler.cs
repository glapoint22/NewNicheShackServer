using MediatR;
using Website.Domain.Events;

namespace Website.Application.ProductReviews.PostReview.EventHandlers
{
    public sealed class PostedReviewEventHandler : INotificationHandler<PostedReviewEvent>
    {
        public Task Handle(PostedReviewEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}