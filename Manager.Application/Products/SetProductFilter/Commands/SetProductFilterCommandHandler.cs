using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductFilter.Commands
{
    public sealed class SetProductFilterCommandHandler : IRequestHandler<SetProductFilterCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetProductFilterCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
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

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}