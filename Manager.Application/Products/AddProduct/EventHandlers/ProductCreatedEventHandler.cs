using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;

namespace Manager.Application.Products.AddProduct.EventHandlers
{
    public sealed class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly IManagerDbContext _dbContext;

        public ProductCreatedEventHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            Publish publish = Publish.AddProduct(notification.ProductId, notification.UserId, PublishStatus.New);

            _dbContext.Publishes.Add(publish);
            await _dbContext.SaveChangesAsync();
        }
    }
}