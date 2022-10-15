using MediatR;
using Quartz;
using Website.Infrastructure.Persistence.Interceptors;

namespace Website.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessDomainEventsJob : IJob
    {
        private readonly IPublisher _publisher;

        public ProcessDomainEventsJob(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<INotification> domainEvents = DomainEventsInterceptor.GetDomainEvents();

            if (domainEvents.Count > 0)
            {
                foreach (var domainEvent in domainEvents)
                {
                    await _publisher.Publish(domainEvent);
                }
            }
        }
    }
}