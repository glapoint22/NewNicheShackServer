using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Common.Interfaces;

namespace Shared.Common.Interceptors
{
    public sealed class DomainEventsInterceptor : SaveChangesInterceptor
    {
        private static readonly List<INotification> _domainEvents = new();

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext != null)
            {
                IEnumerable<IEntity> entities = dbContext.ChangeTracker
                        .Entries<IEntity>()
                        .Select(e => e.Entity);

                List<INotification> domainEvents = entities
                    .SelectMany(e => e.DomainEvents)
                    .ToList();

                _domainEvents.AddRange(domainEvents);

                if (domainEvents.Count > 0)
                {
                    entities.ToList().ForEach(e => e.ClearDomainEvents());
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }



        public static List<INotification> GetDomainEvents()
        {
            List<INotification> domainEvents = new();

            domainEvents.AddRange(_domainEvents);

            _domainEvents.Clear();

            return domainEvents;

        }


        public static void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}