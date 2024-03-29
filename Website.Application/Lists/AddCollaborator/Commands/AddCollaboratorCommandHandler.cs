﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddCollaborator.Commands
{
    public sealed class AddCollaboratorCommandHandler : IRequestHandler<AddCollaboratorCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddCollaboratorCommandHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddCollaboratorCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            List? list = await _dbContext.Lists
                .Where(x => x.CollaborateId == request.CollaborateId)
                .Include(x => x.Collaborators)
                .SingleOrDefaultAsync();

            if (list == null) return Result.Failed();

            // Add the collaborator
            bool succeeded = list.AddCollaborator(userId, false);
            if (!succeeded) return Result.Failed();


            // Add the domain event
            list.AddDomainEvent(new CollaboratorAddedToListEvent(list.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}