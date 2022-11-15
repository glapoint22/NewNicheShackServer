using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.ProductReviews.PostReview.EventHandlers
{
    public sealed class PostedReviewNotificationEventHandler : INotificationHandler<PostedReviewEvent>
    {
        private readonly IWebsiteDbContext _dbContext;

        public PostedReviewNotificationEventHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(PostedReviewEvent notification, CancellationToken cancellationToken)
        {
            // Check to see if this user is blocked
            if (await _dbContext.Users.AnyAsync(x => x.Id == notification.UserId && x.BlockNotificationSending)) return;


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.Review && x.UserId == notification.UserId && x.ProductId == notification.ProductId)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification postedReviewNotification = Notification.CreateReviewNotification(notificationGroupId, notification.UserId, notification.ProductId, notification.ReviewId);
            _dbContext.Notifications.Add(postedReviewNotification);

            await _dbContext.SaveChangesAsync();
        }
    }
}