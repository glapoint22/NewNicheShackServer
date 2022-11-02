using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Shared.Common.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Notifications.PostNotification.Commands
{
    public sealed class PostNotificationCommandHandler : IRequestHandler<PostNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public PostNotificationCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(PostNotificationCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // If a message is being sent from a non-account user
            if (request.NonAccountEmail != null)
            {
                // Check to see if the email of that non-account user is on the block list
                bool isBlockedNonAccountEmail = await _dbContext.BlockedNonAccountEmails
                    .AnyAsync(x => x.Email == request.NonAccountEmail);

                // If so, don't create the notification
                if (isBlockedNonAccountEmail) return Result.Succeeded();

                // If it's anything other than a message from a non-account user
            }
            else
            {
                // Check to see if the user is on the block list
                bool blockedUser = await _dbContext.Users
                    .AnyAsync(x => x.Id == userId && x.BlockNotificationSending);

                // If so, don't create the notification
                if (blockedUser) return Result.Succeeded();
            }


            // First, check to see if a notification group for the type of notification that we're going to create already exists
            NotificationGroup? notificationGroup = await _dbContext.Notifications.Where(x =>

            // If we're creating UserName or UserImage notification
            ((request.Type == (int)NotificationType.UserName || request.Type == (int)NotificationType.UserImage) && x.Type == request.Type && x.UserId == userId) ||

            // If we're creating a Message notification
            (request.Type == (int)NotificationType.Message &&
            // If it's a message from a user with an account
                    (request.NonAccountEmail == null && x.Type == request.Type && x.User.Email == request.Email) ||
            // Or if it's a message from a user with (NO) account
                    (request.NonAccountEmail != null && x.Type == request.Type && x.NonAccountEmail == request.NonAccountEmail)) ||

            // If we're creating a Review notification
            (request.Type == (int)NotificationType.Review && x.Type == request.Type && x.ReviewId == request.ReviewId) ||

            // If we're creating a Product notification
            (request.Type > (int)NotificationType.Review && request.Type < (int)NotificationType.Error && x.Type == request.Type && x.ProductId == request.ProductId))
                .Select(x => x.NotificationGroup).FirstOrDefaultAsync();





            // If a notification group does (NOT) exists
            // OR the notification type we're creating is an Error notification
            if (notificationGroup == null || request.Type == (int)NotificationType.Error)
            {
                // Then create a new notification group
                notificationGroup = new NotificationGroup()
                {
                    Id = Guid.NewGuid()
                };

                _dbContext.NotificationGroups.Add(notificationGroup);
            }


            // Now create the new notification
            var notification = new Notification()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroup.Id,
                UserId = userId,
                ProductId = request.ProductId,
                ReviewId = request.ReviewId,
                Type = request.Type,
                UserName = request.UserName,
                UserImage = request.UserImage,
                Text = request.Text,
                NonAccountName = request.NonAccountName,
                NonAccountEmail = request.NonAccountEmail,
                CreationDate = DateTime.UtcNow
            };
            _dbContext.Notifications.Add(notification);


            // Save
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}