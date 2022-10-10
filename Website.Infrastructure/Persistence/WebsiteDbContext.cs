using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using Website.Application.Common.Interfaces;
using Website.Domain.Common;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence
{
    public class WebsiteDbContext : IdentityDbContext<User>, IWebsiteDbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        public DbSet<Collaborator> Collaborators => Set<Collaborator>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<List> Lists => Set<List>();

        private readonly IPublisher _publisher;

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public async Task<int> SaveChangesAsync()
        {
            IEnumerable<IEntity> entities = ChangeTracker
                    .Entries<IEntity>()
                    .Select(e => e.Entity);

            List<INotification> domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            if (domainEvents.Count > 0)
            {
                entities.ToList().ForEach(e => e.ClearDomainEvents());

                foreach (var domainEvent in domainEvents)
                    await _publisher.Publish(domainEvent);
            }

            return await base.SaveChangesAsync();
        }
    }
}