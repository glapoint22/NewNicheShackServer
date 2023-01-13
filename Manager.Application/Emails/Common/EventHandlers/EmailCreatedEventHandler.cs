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
            PublishItem publishItem = PublishItem.AddEmail(notification.EmailId, notification.UserId, PublishStatus.New);

            _dbContext.PublishItems.Add(publishItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}