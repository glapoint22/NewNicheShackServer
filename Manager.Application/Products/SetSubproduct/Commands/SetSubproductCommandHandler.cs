using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetSubproduct.Commands
{
    public sealed record SetSubproductCommandHandler : IRequestHandler<SetSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetSubproductCommand request, CancellationToken cancellationToken)
        {
            Subproduct subproduct = (await _dbContext.Subproducts.FindAsync(request.SubproductId))!;
            subproduct.Set(request.Name, request.Description, request.ImageId, request.Value);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}