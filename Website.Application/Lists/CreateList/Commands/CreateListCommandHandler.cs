using MediatR;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Lists.CreateList.Commands
{
    public sealed class CreateListCommandHandler : IRequestHandler<CreateListCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public CreateListCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Create the list
            List list = new(request.Name, request.Description);

            // Add this user as a collaborator to the list
            Collaborator collaborator = new(list.Id, userId, true);

            _dbContext.Lists.Add(list);
            _dbContext.Collaborators.Add(collaborator);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                list.Id,
                list.CollaborateId
            });
        }
    }
}