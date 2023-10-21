using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetUserNameNotification.Queries
{
    public sealed class GetUserNameNotificationQueryHandler : IRequestHandler<GetUserNameNotificationQuery, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public GetUserNameNotificationQueryHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }


        public async Task<Result> Handle(GetUserNameNotificationQuery request, CancellationToken cancellationToken)
        {
            // Get the user notifications from the website
            var notifications = await _websiteDbContext.Notifications
                .OrderByDescending(x => x.CreationDate)
                .Where(x => x.NotificationGroupId == request.NotificationGroupId && (request.IsNew ? !x.IsArchived : x.IsArchived))
                .Select(x => new
                {
                    NotificationId = x.Id,
                    x.UserId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    x.Text,
                    Date = x.CreationDate,
                    x.User.NoncompliantStrikes,
                    x.User.BlockNotificationSending,
                    UserName = x.Name,
                    EmployeeIndex = 0
                }).ToListAsync();

            // Get the employee notes from this notification
            var employeeNotes = await _managerDbContext.NotificationEmployeeNotes
                .OrderByDescending(x => x.CreationDate)
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
                    x.UserId,
                    x.FirstName,
                    x.LastName,
                    x.Image,
                    x.Email,
                    x.Text,
                    x.Date,
                    x.BlockNotificationSending,
                    x.UserName,
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