using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddSubproduct.Commands
{
    public sealed class AddSubproductCommandHandler : IRequestHandler<AddSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddSubproductCommand request, CancellationToken cancellationToken)
        {
            Subproduct subproduct = Subproduct.Create(request.ProductId, request.Type);
            _dbContext.Subproducts.Add(subproduct);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(subproduct.Id);
        }
    }
}