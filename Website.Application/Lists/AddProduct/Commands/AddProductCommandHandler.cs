using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddProduct.Commands
{
    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddProductCommandHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            // Get the user id from claims
            string userId = _authService.GetUserIdFromClaims();

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
            if (!succeeded) return Result.Failed("409");

            // Add the domain event
            list.AddDomainEvent(new ProductAddedToListEvent(request.ListId, request.ProductId, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}