using MediatR;
using Shared.Common.Entities;
using Website.Application.Common.Interfaces;

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
                _dbContext.Notifications.Add(notification);

                await _dbContext.SaveChangesAsync();
                throw;
            }
        }
    }
}
