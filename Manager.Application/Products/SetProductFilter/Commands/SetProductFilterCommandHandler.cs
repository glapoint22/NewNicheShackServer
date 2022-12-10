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
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductFilters)
                .SingleAsync();

            product.SetProductFilter(request.FilterOptionId, request.Checked);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}