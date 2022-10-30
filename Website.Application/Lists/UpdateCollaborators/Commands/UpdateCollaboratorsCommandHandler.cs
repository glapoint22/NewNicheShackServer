using MediatR;
using Microsoft.EntityFrameworkCore;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.UpdateCollaborators.Classes;
using Shared.Common.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.UpdateCollaborators.Commands
{
    public sealed class UpdateCollaboratorsCommandHandler : IRequestHandler<UpdateCollaboratorsCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public UpdateCollaboratorsCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateCollaboratorsCommand request, CancellationToken cancellationToken)
        {
            // Grab the collaborators from the database
            List<Collaborator> collaborators = await _dbContext.Collaborators
                .Where(x => request.UpdatedCollaborators
                    .Select(z => z.Id)
                    .Contains(x.Id))
                .ToListAsync();

            // Update each collaborator
            foreach (var collaborator in collaborators)
            {
                UpdatedCollaborator? updatedCollaborator = request.UpdatedCollaborators
                    .FirstOrDefault(z => z.Id == collaborator.Id);

                if (updatedCollaborator != null)
                {
                    if (updatedCollaborator.IsRemoved)
                    {
                        _dbContext.Collaborators.Remove(collaborator);

                        // Add the event
                        collaborator.AddDomainEvent(new CollaboratorUpdatedEvent(collaborator.UserId, request.ListId, true));
                    }
                    else
                    {
                        collaborator.CanDeleteList = updatedCollaborator.ListPermissions.CanDeleteList;
                        collaborator.CanShareList = updatedCollaborator.ListPermissions.CanShareList;
                        collaborator.CanEditList = updatedCollaborator.ListPermissions.CanEditList;
                        collaborator.CanAddToList = updatedCollaborator.ListPermissions.CanAddToList;
                        collaborator.CanInviteCollaborators = updatedCollaborator.ListPermissions.CanInviteCollaborators;
                        collaborator.CanManageCollaborators = updatedCollaborator.ListPermissions.CanManageCollaborators;
                        collaborator.CanRemoveItem = updatedCollaborator.ListPermissions.CanRemoveItem;

                        // add the event
                        collaborator.AddDomainEvent(new CollaboratorUpdatedEvent(collaborator.UserId, request.ListId));
                    }
                }

                await _dbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}