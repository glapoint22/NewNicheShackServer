using MediatR;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Shared.Common.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddProduct.Commands
{
    public sealed class AddProductCommandHandler : CollaboratorProductHandler, IRequestHandler<AddProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public AddProductCommandHandler(IWebsiteDbContext dbContext, IUserService userService) : base(dbContext, userService)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            // Add the new product
            CollaboratorProduct collaboratorProduct = AddProduct(request.ProductId, request.CollaboratorId);
            collaboratorProduct.AddDomainEvent(new ProductAddedToListEvent(collaboratorProduct.Id));

            // Save the changes
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}