using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.AddProduct.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public AddProductCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();
            int collaboratorId = await _dbContext.Collaborators
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .SingleAsync();

            CollaboratorProduct collaboratorProduct = new(request.ProductId, collaboratorId);
            collaboratorProduct.AddDomainEvent(new ProductAddedToListEvent(request.ListId, request.ProductId, userId));


            _dbContext.CollaboratorProducts.Add(collaboratorProduct);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}