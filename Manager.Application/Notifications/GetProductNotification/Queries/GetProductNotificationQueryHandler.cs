using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Notifications.GetProductNotification.Queries
{
    public sealed class GetProductNotificationQueryHandler : IRequestHandler<GetProductNotificationQuery, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public GetProductNotificationQueryHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }


        public async Task<Result> Handle(GetProductNotificationQuery request, CancellationToken cancellationToken)
        {









            var product = await _websiteDbContext.Notifications
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.Product.Hoplink,
                    x.Product.Disabled
                }).FirstAsync();




            var users = await _websiteDbContext.Notifications
                .OrderByDescending(x => x.CreationDate)
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.UserId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    x.Text,
                    Date = x.CreationDate,
                    x.User.NoncompliantStrikes,
                    x.User.BlockNotificationSending
                }).ToListAsync();

            var employeeNotes = await _managerDbContext.NotificationEmployeeNotes
                .OrderByDescending(x => x.CreationDate)
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
                ProductHoplink = product.Hoplink,
                ProductDisabled = product.Disabled,
                Users = users,
                EmployeeNotes = employeeNotes
            });
        }
    }
}