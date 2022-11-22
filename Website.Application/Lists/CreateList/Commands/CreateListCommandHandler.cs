using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.CreateList.Commands
{
    public sealed class CreateListCommandHandler : IRequestHandler<CreateListCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public CreateListCommandHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            // Create the list
            List list = List.Create(request.Name, request.Description);

            // Add this user as a collaborator to the list
            Collaborator collaborator = Collaborator.Create(list.Id, userId, true);

            _dbContext.Lists.Add(list);
            _dbContext.Collaborators.Add(collaborator);

            list.AddDomainEvent(new ListEvent(userId, list.Id, request.Name, request.Description));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                list.Id,
                list.CollaborateId
            });
        }
    }
}