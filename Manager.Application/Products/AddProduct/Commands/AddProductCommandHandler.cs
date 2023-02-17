using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddProduct.Commands
{
    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;
        public AddProductCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product product = Product.Create(request.Id, request.Name);
            _dbContext.Products.Add(product);

            string userId = _authService.GetUserIdFromClaims();

            product.AddDomainEvent(new ProductCreatedEvent(request.Name, product.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(product.Id);
        }
    }
}