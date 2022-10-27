using MediatR;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.RemoveProduct.Commands
{
    public sealed class RemoveProductCommandHandler : CollaboratorProductHandler, IRequestHandler<RemoveProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public RemoveProductCommandHandler(IWebsiteDbContext dbContext, IUserService userService) : base(dbContext, userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Remove the product
            CollaboratorProduct product = await RemoveProduct(request.CollaboratorProductId);
            product.AddDomainEvent(new ProductRemovedFromListEvent(product.Collaborator.ListId, product.ProductId, userId));

            // Save changes
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}