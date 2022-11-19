using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Notifications.PostMessageNotification.Commands
{
    public sealed class PostMessageNotificationCommandHandler : IRequestHandler<PostMessageNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public PostMessageNotificationCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(PostMessageNotificationCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Check to see if the user is on the block list
            if (await _dbContext.Users
                .AnyAsync(x => x.Id == userId && x.BlockNotificationSending)) return Result.Succeeded();


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.Message && x.UserId == userId)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification notification = Notification.CreateMessageNotification(notificationGroupId, userId, request.Text);

            _dbContext.Notifications.Add(notification);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}