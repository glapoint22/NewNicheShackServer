using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Notifications.PostNonAccountMessageNotification.Commands
{
    public sealed class PostNonAccountMessageNotificationCommandHandler : IRequestHandler<PostNonAccountMessageNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public PostNonAccountMessageNotificationCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(PostNonAccountMessageNotificationCommand request, CancellationToken cancellationToken)
        {
            // Check to see if the email of the non-account user is on the block list
            if (await _dbContext.BlockedNonAccountUsers
                .AnyAsync(x => x.Email == request.Email)) return Result.Succeeded();


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.Message && x.NonAccountEmail == request.Email)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification notification = Notification.CreateNonAccountMessageNotification(notificationGroupId, request.Name, request.Email, request.Text);

            _dbContext.Notifications.Add(notification);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}