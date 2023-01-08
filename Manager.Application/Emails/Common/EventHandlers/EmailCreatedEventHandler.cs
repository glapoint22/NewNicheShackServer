using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;

namespace Manager.Application.Emails.Common.EventHandlers
{
    public sealed class EmailCreatedEventHandler : INotificationHandler<EmailCreatedEvent>
    {
        private readonly IManagerDbContext _dbContext;

        public EmailCreatedEventHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(EmailCreatedEvent notification, CancellationToken cancellationToken)
        {
            Publish publish = Publish.AddEmail(notification.EmailId, notification.UserId, PublishStatus.New);

            _dbContext.Publishes.Add(publish);
            await _dbContext.SaveChangesAsync();
        }
    }
}