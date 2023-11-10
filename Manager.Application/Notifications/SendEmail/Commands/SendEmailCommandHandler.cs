using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using IAuthService = Manager.Application.Common.Interfaces.IAuthService;

namespace Manager.Application.Notifications.SendEmail.Commands
{
    public sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IAuthService _authService;

        public SendEmailCommandHandler(IManagerDbContext managerDbContext, IAuthService authService, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _authService = authService;
            _websiteDbContext = websiteDbContext;
        }


        public async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            NotificationEmployeeNote notificationEmployeeNote = NotificationEmployeeNote
                .Create(request.NotificationGroupId, request.NotificationId, userId, request.Email);

            _managerDbContext.NotificationEmployeeNotes.Add(notificationEmployeeNote);



            Website.Domain.Entities.Notification? notification = await _websiteDbContext.Notifications
                .Where(x => x.Id == request.NotificationId)
                .SingleOrDefaultAsync();


            if (notification != null && notification.Text != null)
            {
                string recpientName = string.Empty;
                string recpientEmailAddress = string.Empty;

                if (notification.NonAccountName != null && notification.NonAccountEmail != null)
                {
                    recpientName = notification.NonAccountName;
                    recpientEmailAddress = notification.NonAccountEmail;
                }
                else
                {
                    var user = await _websiteDbContext.Users
                        .Where(x => x.Id == notification.UserId)
                        .SingleOrDefaultAsync();

                    if (user == null) return Result.Succeeded();

                    recpientName = user.FirstName;
                    recpientEmailAddress = user.Email;
                }

                var employeeEmailAddress = await _managerDbContext.Users
                    .Where(x => x.Id == userId)
                    .Select(x => x.Email)
                    .SingleOrDefaultAsync();

                if (employeeEmailAddress == null) return Result.Succeeded();

                DomainEventsInterceptor.AddDomainEvent(new UserSentEmailEvent(notification.Text, recpientEmailAddress, recpientName, employeeEmailAddress, request.Email));
            }



            await _managerDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}
