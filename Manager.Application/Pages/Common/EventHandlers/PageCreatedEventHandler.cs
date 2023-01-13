using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;

namespace Manager.Application.Pages.Common.EventHandlers
{
    public sealed class PageCreatedEventHandler : INotificationHandler<PageCreatedEvent>
    {
        private readonly IManagerDbContext _dbContext;

        public PageCreatedEventHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(PageCreatedEvent notification, CancellationToken cancellationToken)
        {
            PublishItem publishItem = PublishItem.AddPage(notification.PageId, notification.UserId, PublishStatus.New);

            _dbContext.PublishItems.Add(publishItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}