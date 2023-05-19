using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Common.Behaviors
{
    public sealed class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IWebsiteDbContext _dbContext;

        public UnhandledExceptionBehavior(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception error)
            {
                // Create the notification
                Notification notification = Notification.CreateErrorNotification(error);


                // Check if there is already an error notification with the same text and is not archived
                if (!await _dbContext.Notifications.AnyAsync(x => x.Type == (int)NotificationType.Error && x.NotificationGroup.ArchiveDate == null && x.Text == notification.Text))
                {
                    _dbContext.Notifications.Add(notification);
                    await _dbContext.SaveChangesAsync();
                }
                
                throw;
            }
        }
    }
}
