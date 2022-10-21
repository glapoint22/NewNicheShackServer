using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.MoveProduct.Commands
{
    public sealed class MoveProductCommandHandler : CollaboratorProductHandler, IRequestHandler<MoveProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public MoveProductCommandHandler(IWebsiteDbContext dbContext, IUserService userService) : base(dbContext)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(MoveProductCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();
            CollaboratorProduct sourceCollaboratorProduct = await RemoveProduct(request.CollaboratorProductId);

            int collaboratorId = await _dbContext.Collaborators
                .Where(x => x.UserId == userId && x.ListId == request.DestinationListId)
                .Select(x => x.Id)
                .SingleAsync();


            CollaboratorProduct destinationCollaboratorProduct = AddProduct(sourceCollaboratorProduct.ProductId, collaboratorId);

            destinationCollaboratorProduct.AddDomainEvent(new ProductMovedToListEvent(sourceCollaboratorProduct.Collaborator.ListId, destinationCollaboratorProduct.Id));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}