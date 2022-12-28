using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetNotificationCount.Queries
{
    public sealed class GetNotificationCountQueryHandler : IRequestHandler<GetNotificationCountQuery, int>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetNotificationCountQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(GetNotificationCountQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Notifications
                .CountAsync(x =>

            // Count all the notifications that (DO NOT) belong to an archived group
            x.NotificationGroup.ArchiveDate == null ||


            // but if it's a UserName, UserImage, or a Message that (DOES) belong to an
            // archive group and that notification has (NOT) been archived, then count that one too
            (x.Type == (int)NotificationType.UserName ||
            x.Type == (int)NotificationType.UserImage ||
            x.Type == (int)NotificationType.List ||
            x.Type == (int)NotificationType.Review ||
            x.Type == (int)NotificationType.Message) &&
            !x.IsArchived);
        }
    }
}