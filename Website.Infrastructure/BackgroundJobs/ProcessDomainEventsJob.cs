using MediatR;
using Quartz;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public sealed class ProcessDomainEventsJob : IJob
    {
        private readonly IPublisher _publisher;
        private readonly IWebsiteDbContext _dbContext;

        public ProcessDomainEventsJob(IPublisher publisher, IWebsiteDbContext dbContext)
        {
            _publisher = publisher;
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<INotification> domainEvents = DomainEventsInterceptor.GetDomainEvents();

            if (domainEvents.Count > 0)
            {
                foreach (var domainEvent in domainEvents)
                {
                    try
                    {
                        await _publisher.Publish(domainEvent);
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
    }
}