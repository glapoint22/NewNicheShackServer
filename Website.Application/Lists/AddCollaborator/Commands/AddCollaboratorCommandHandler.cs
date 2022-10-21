using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddCollaborator.Commands
{
    public sealed class AddCollaboratorCommandHandler : IRequestHandler<AddCollaboratorCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public AddCollaboratorCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(AddCollaboratorCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();
            string listId = await _dbContext.Lists
                .Where(x => x.CollaborateId == request.CollaborateId)
                .Select(x => x.Id)
                .SingleAsync();

            // Add the new collaborator
            Collaborator collaborator = new(listId, userId);
            _dbContext.Collaborators.Add(collaborator);

            // Add the domain event
            collaborator.AddDomainEvent(new CollaboratorAddedToListEvent(listId, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}