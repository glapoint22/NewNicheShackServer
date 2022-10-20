using MediatR;
using Website.Domain.Events;

namespace Website.Application.Lists.AddCollaborator.EventHandlers
{
    public class CollaboratorAddToListEventHandler : INotificationHandler<CollaboratorAddedToListEvent>
    {
        public Task Handle(CollaboratorAddedToListEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}