using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveSubproduct.Commands
{
    public sealed class RemoveSubproductCommandHandler : IRequestHandler<RemoveSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public RemoveSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveSubproductCommand request, CancellationToken cancellationToken)
        {
            Subproduct subproduct = (await _dbContext.Subproducts.FindAsync(request.SubproductId))!;
            _dbContext.Subproducts.Remove(subproduct);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}