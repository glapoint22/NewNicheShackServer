using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.MoveProduct.Commands
{
    public sealed class MoveProductCommandHandler : IRequestHandler<MoveProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public MoveProductCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(MoveProductCommand request, CancellationToken cancellationToken)
        {
            // Get user id from claims
            string userId = _userService.GetUserIdFromClaims();


            List<List> lists = await _dbContext.Lists
                .Where(x => x.Id == request.SourceListId || x.Id == request.DestinationListId)
                .Include(x => x.Products)
                .Include(x => x.Collaborators
                    .Where(z => z.UserId == userId))
                .ToListAsync();

            // If there are not two lists
            if (lists.Count != 2) return Result.Failed();


            // Get the source list
            List sourceList = lists
                .Where(x => x.Id == request.SourceListId)
                .Single();

            // Try removing the product from the list
            bool succeeded = sourceList.RemoveProduct(request.ProductId, sourceList.Collaborators[0]);

            // If not succeeded
            if (!succeeded) return Result.Failed();

            // Get the destination list
            List destinationList = lists
                .Where(x => x.Id == request.DestinationListId)
                .Single();

            // Try adding the product to the list
            succeeded = destinationList.AddProduct(request.ProductId, destinationList.Collaborators[0]);

            // If not succeeded
            if (!succeeded) return Result.Failed("409");


            destinationList.AddDomainEvent(new ProductMovedToListEvent(request.SourceListId, request.DestinationListId, request.ProductId, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}