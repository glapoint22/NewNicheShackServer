using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetMessageNotification.Queries
{
    public sealed class GetMessageNotificationQueryHandler : IRequestHandler<GetMessageNotificationQuery, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public GetMessageNotificationQueryHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }



        public async Task<Result> Handle(GetMessageNotificationQuery request, CancellationToken cancellationToken)
        {
            // Get the notifications
            var notifications = await _websiteDbContext.Notifications
                .Where(x => x.NotificationGroupId == request.NotificationGroupId && (request.IsNew ? !x.IsArchived : x.IsArchived))
                .Select(x => new
                {
                    NotificationId = x.Id,
                    x.UserId,
                    x.NonAccountName,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    Email = x.NonAccountEmail ?? x.User.Email,
                    x.Text,
                    Date = x.CreationDate,
                    NoncompliantStrikes = x.User != null ? x.User.NoncompliantStrikes : 0,
                    BlockNotificationSending = x.User != null ? x.User.BlockNotificationSending : _websiteDbContext.BlockedNonAccountUsers
                        .Any(z => z.Email == x.NonAccountEmail)
                }).ToListAsync();

            // Get the employee notes
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
            var messageNotifications = notifications
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
                    x.NoncompliantStrikes,
                    x.NonAccountName,
                    EmployeeMessage = y.FirstOrDefault()
                })
                .OrderByDescending(x => x.Date)
                .ToList();

            return Result.Succeeded(messageNotifications);
        }
    }
}