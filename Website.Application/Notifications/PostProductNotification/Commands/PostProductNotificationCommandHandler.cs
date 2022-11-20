using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Notifications.PostProductNotification.Commands
{
    public sealed class PostProductNotificationCommandHandler : IRequestHandler<PostProductNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public PostProductNotificationCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }



        public async Task<Result> Handle(PostProductNotificationCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Check to see if the user is on the block list
            if (await _dbContext.Users
                .AnyAsync(x => x.Id == userId && x.BlockNotificationSending)) return Result.Succeeded();


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == request.Type && x.ProductId == request.ProductId)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification notification = Notification.CreateProductNotification(notificationGroupId, userId, request.ProductId, request.Type, request.Text);
            _dbContext.Notifications.Add(notification);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}