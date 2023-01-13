using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Products.Common.EventHandlers
{
    public sealed class ProductModifiedEventHandler : INotificationHandler<ProductModifiedEvent>
    {
        private readonly IManagerDbContext _dbContext;

        public ProductModifiedEventHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductModifiedEvent notification, CancellationToken cancellationToken)
        {
            if (!await _dbContext.PublishItems.AnyAsync(x => x.ProductId == notification.ProductId))
            {
                PublishItem publishItem = PublishItem.AddProduct(notification.ProductId, notification.UserId, PublishStatus.Modified);

                _dbContext.PublishItems.Add(publishItem);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}