using MediatR;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.EventHandlers
{
    public class ActivateAccountCommandCreateListHandler : INotificationHandler<ActivateAccountSucceededEvent>
    {
        private readonly IWebsiteDbContext _context;

        public ActivateAccountCommandCreateListHandler(IWebsiteDbContext context)
        {
            _context = context;
        }

        public async Task Handle(ActivateAccountSucceededEvent notification, CancellationToken cancellationToken)
        {
            List list = new()
            {
                Id = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                Name = notification.User.FirstName + "'s List",
                Description = string.Empty,
                CollaborateId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper()
            };

            _context.Lists.Add(list);


            Collaborator collaborator = new()
            {
                UserId = notification.User.Id,
                ListId = list.Id,
                IsOwner = true,
                AddToList = true,
                ShareList = true,
                EditList = true,
                InviteCollaborators = true,
                DeleteList = true,
                MoveItem = true,
                RemoveItem = true
            };

            _context.Collaborators.Add(collaborator);

            await _context.SaveChangesAsync();
        }
    }
}
