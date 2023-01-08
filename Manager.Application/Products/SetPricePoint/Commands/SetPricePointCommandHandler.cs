using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetPricePoint.Commands
{
    public sealed class SetPricePointCommandHandler : IRequestHandler<SetPricePointCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetPricePointCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetPricePointCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.PricePoints
                    .Where(z => z.Id == request.PricePoint.Id))
                    .ThenInclude(z => z.ProductPrice)
                .SingleAsync();

            product.SetPricePoint(request.PricePoint);

            string userId = _authService.GetUserIdFromClaims();
            product.AddDomainEvent(new ProductModifiedEvent(product.Id, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}