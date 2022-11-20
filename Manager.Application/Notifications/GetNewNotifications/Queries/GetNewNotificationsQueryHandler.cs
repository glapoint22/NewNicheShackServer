using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetNewNotifications.Queries
{
    public sealed class GetNewNotificationsQueryHandler : IRequestHandler<GetNewNotificationsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetNewNotificationsQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetNewNotificationsQuery request, CancellationToken cancellationToken)
        {
            var allNotifications = await _dbContext.Notifications.Where(x =>
            // Get all the notifications that (DO NOT) belong to an archived group
            x.NotificationGroup.ArchiveDate == null ||


            // but if it's a UserName, UserImage, or a Message that (DOES) belong to an
            // archive group and that notification has (NOT) been archived, then count that one too
            (x.Type == (int)NotificationType.UserName ||
            x.Type == (int)NotificationType.UserImage ||
            x.Type == (int)NotificationType.Message) &&
            !x.IsArchived)
                .Select(x => new
                {
                    x.Id,
                    x.NotificationGroupId,
                    Email = x.Type == (int)NotificationType.Message ?
                        x.NonAccountEmail != null ?
                        x.NonAccountEmail :
                        x.User.Email :
                        null,
                    x.ProductId,
                    ProductName = x.Product.Name,
                    ProductImage = x.Product.Media.Thumbnail,
                    x.UserImage,
                    NotificationType = x.Type,
                    x.CreationDate,
                    x.NotificationGroup.ArchiveDate,
                    Count =
                        x.Type == (int)NotificationType.UserName || x.Type == (int)NotificationType.UserImage || x.Type == (int)NotificationType.Message ?
                            x.NotificationGroup.Notifications.Where(y => !y.IsArchived).Count() :
                            x.NotificationGroup.Notifications.Count()
                }).ToListAsync();

            var notifications = allNotifications
            // Group each notification by the group they belong to
            .GroupBy(x => x.NotificationGroupId, (key, n) => n
            // Then order each notification in each group by the most recent date
            .OrderByDescending(y => y.CreationDate)
            // And then return a list that consists of only the first notification of each group
            .First())
            // Then take that list and order it by either the creation date (if we're compiling a New list) or the archive date (if we're compiling an Archive list)
            .OrderByDescending(x => x.CreationDate)
            .Select(x => new
            {
                x.Id,
                x.NotificationGroupId,
                x.NotificationType,
                x.Email,
                x.ProductId,
                x.ProductName,
                Image = x.ProductImage,
                x.UserImage,
                IsNew = true,
                x.CreationDate,
                x.Count
            }).ToList();

            return Result.Succeeded(new
            {
                notifications.Count,
                notifications
            });
        }
    }
}