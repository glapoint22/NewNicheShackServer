using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.AddProduct.EventHandlers
{
    public sealed class ProductAddedToListEventHandler : INotificationHandler<ProductAddedToListEvent>
    {
        public Task Handle(ProductAddedToListEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}