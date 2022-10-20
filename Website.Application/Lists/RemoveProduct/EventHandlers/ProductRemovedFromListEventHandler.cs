using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.RemoveProduct.EventHandlers
{
    public class ProductRemovedFromListEventHandler : INotificationHandler<ProductRemovedFromListEvent>
    {
        public Task Handle(ProductRemovedFromListEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}