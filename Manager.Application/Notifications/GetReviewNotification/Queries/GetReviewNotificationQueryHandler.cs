using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetReviewNotification.Queries
{
    public sealed class GetReviewNotificationQueryHandler : IRequestHandler<GetReviewNotificationQuery, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public GetReviewNotificationQueryHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }


        public async Task<Result> Handle(GetReviewNotificationQuery request, CancellationToken cancellationToken)
        {
            // Get the user notifications from the website
            var notifications = await _websiteDbContext.Notifications
                .Where(x => x.NotificationGroupId == request.NotificationGroupId && (request.IsNew ? !x.IsArchived : x.IsArchived))
                .Select(x => new
                {
                    NotificationId = x.Id,
                    x.ReviewId,
                    x.UserId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    x.ProductReview.Text,
                    Date = x.CreationDate,
                    x.User.NoncompliantStrikes,
                    x.User.BlockNotificationSending,
                    x.ProductReview.Title,
                    EmployeeIndex = 0
                }).ToListAsync();

            // Get the employee notes from this notification
            var employeeNotes = await _managerDbContext.NotificationEmployeeNotes
                .Where(x => notifications.Select(z => z.NotificationId).ToList().Contains((Guid)x.NotificationId!))
                .Select(x => new
                {
                    x.NotificationId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    Text = x.Note,
                    Date = x.CreationDate
                }).ToListAsync();


            // Join the two
            var userNameNotifications = notifications
                .GroupJoin(employeeNotes, x => x.NotificationId, x => x.NotificationId, (x, y) => new
                {
                    x.NotificationId,
                    x.ReviewId,
                    x.UserId,
                    x.FirstName,
                    x.LastName,
                    x.Image,
                    x.Email,
                    x.Text,
                    x.Date,
                    x.BlockNotificationSending,
                    x.Title,
                    x.EmployeeIndex,
                    x.NoncompliantStrikes,
                    EmployeeNotes = y.ToList()
                })
                .OrderByDescending(x => x.Date)
                .ToList();

            return Result.Succeeded(userNameNotifications);
        }
    }
}