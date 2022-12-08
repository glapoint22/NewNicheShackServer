using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.PostPrice.Commands
{
    public sealed class PostPriceCommandHandler : IRequestHandler<PostPriceCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public PostPriceCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(PostPriceCommand request, CancellationToken cancellationToken)
        {
            ProductPrice productPrice = ProductPrice.Create(request.ProductId, request.Price);

            _dbContext.ProductPrices.Add(productPrice);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}