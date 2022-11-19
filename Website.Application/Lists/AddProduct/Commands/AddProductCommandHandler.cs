using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddProduct.Commands
{
    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public AddProductCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            // Get the user id from claims
            string userId = _userService.GetUserIdFromClaims();

            List? list = await _dbContext.Lists
                .Where(x => x.Id == request.ListId)
                .Include(x => x.Products)
                .Include(x => x.Collaborators
                    .Where(z => z.UserId == userId))
                .SingleOrDefaultAsync();

            // If null, list has been deleted
            if (list == null) return Result.Failed();

            // Try adding the product to the list
            bool succeeded = list.AddProduct(request.ProductId, list.Collaborators[0]);

            // If not succeeded
            if (!succeeded) return Result.Failed();

            // Add the domain event
            list.AddDomainEvent(new ProductAddedToListEvent(request.ListId, request.ProductId, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}