using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.MoveProduct.EventHandlers
{
    public sealed class ProductMovedToListEventHandler : INotificationHandler<ProductMovedToListEvent>
    {
        public Task Handle(ProductMovedToListEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}