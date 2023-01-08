using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductFilter.Commands
{
    public sealed class SetProductFilterCommandHandler : IRequestHandler<SetProductFilterCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetProductFilterCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetProductFilterCommand request, CancellationToken cancellationToken)
        {
            ProductFilter productFilter;

            if (request.Checked)
            {
                productFilter = ProductFilter.Create(request.ProductId, request.FilterOptionId);
                _dbContext.ProductFilters.Add(productFilter);
            }
            else
            {
                productFilter = await _dbContext.ProductFilters
                    .Where(x => x.ProductId == request.ProductId && x.FilterOptionId == request.FilterOptionId)
                    .SingleAsync();

                _dbContext.ProductFilters.Remove(productFilter);
            }

            string userId = _authService.GetUserIdFromClaims();
            productFilter.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}