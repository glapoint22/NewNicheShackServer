using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Emails.UpdateEmail.EventHandlers
{
    public sealed class EmailModifiedEventHandler : INotificationHandler<EmailModifiedEvent>
    {
        private readonly IManagerDbContext _dbContext;

        public EmailModifiedEventHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(EmailModifiedEvent notification, CancellationToken cancellationToken)
        {
            if (!await _dbContext.PublishItems.AnyAsync(x => x.EmailId == notification.EmailId))
            {
                PublishItem publishItem = PublishItem.AddEmail(notification.EmailId, notification.UserId, PublishStatus.Modified);

                _dbContext.PublishItems.Add(publishItem);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}