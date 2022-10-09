using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Common;

namespace Website.Infrastructure.Persistence.Interceptors
{
    public class Interceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            var events = dbContext.ChangeTracker
                .Entries<IEntity>()
                .Select(x => x.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.DomainEvents;
                    //entity.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();


            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
