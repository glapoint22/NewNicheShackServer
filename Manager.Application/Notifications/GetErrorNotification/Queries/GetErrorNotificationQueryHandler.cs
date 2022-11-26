using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetErrorNotification.Queries
{
    public sealed class GetErrorNotificationQueryHandler : IRequestHandler<GetErrorNotificationQuery, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public GetErrorNotificationQueryHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }

        public async Task<Result> Handle(GetErrorNotificationQuery request, CancellationToken cancellationToken)
        {
            // Get the notification from the website db
            var error = await _websiteDbContext.Notifications
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.Text,
                    Date = x.CreationDate
                }).SingleAsync();


            var employeeNotes = await _managerDbContext.NotificationEmployeeNotes
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    Text = x.Note,
                    Date = x.CreationDate
                }).ToListAsync();

            return Result.Succeeded(new
            {
                error.Text,
                error.Date,
                EmployeeNotes = employeeNotes
            });
        }
    }
}