using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Notifications.PostReviewComplaintNotification.Commands
{
    public sealed class PostReviewComplaintNotificationCommandHandler : IRequestHandler<PostReviewComplaintNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public PostReviewComplaintNotificationCommandHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }



        public async Task<Result> Handle(PostReviewComplaintNotificationCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            // Check to see if the user is on the block list
            if (await _dbContext.Users
                .AnyAsync(x => x.Id == userId && x.BlockNotificationSending)) return Result.Succeeded();


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.ReviewComplaint && x.UserId == userId && x.ProductId == request.ProductId)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification notification = Notification.CreateReviewComplaintNotification(notificationGroupId, userId, request.ProductId, request.ReviewId, request.Text);
            _dbContext.Notifications.Add(notification);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}