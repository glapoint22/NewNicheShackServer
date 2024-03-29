﻿using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Pages.Common.EventHandlers
{
    public sealed class PageModifiedEventHandler : INotificationHandler<PageModifiedEvent>
    {
        private readonly IManagerDbContext _dbContext;

        public PageModifiedEventHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(PageModifiedEvent notification, CancellationToken cancellationToken)
        {
            if (!await _dbContext.PublishItems.AnyAsync(x => x.PageId == notification.PageId))
            {
                PublishItem publishItem = PublishItem.AddPage(notification.PageId, notification.UserId, PublishStatus.Modified);

                _dbContext.PublishItems.Add(publishItem);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}