using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.EditList.Commands
{
    public sealed class EditListCommandHandler : IRequestHandler<EditListCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public EditListCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(EditListCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Get the list to edit
            List? list = await _dbContext.Lists.
                Where(x => x.Id == request.Id && x.Collaborators
                    .Any(z => z.UserId == userId && (z.IsOwner || z.CanEditList)))
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (list == null) return Result.Failed();

            // Edit
            list.Edit(request.Name, request.Description);
            list.AddDomainEvent(new ListEditedEvent(userId, list.Id, list.Name, list.Description, request.Name, request.Description));

            // Save the changes
            _dbContext.Lists.Update(list);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}