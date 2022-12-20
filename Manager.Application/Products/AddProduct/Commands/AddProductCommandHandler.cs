using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddProduct.Commands
{
    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddProductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product product = Product.Create(request.Id, request.Name);

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(product.Id);
        }
    }
}