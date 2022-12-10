using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetVendor.Commands
{
    public sealed class SetVendorCommandHandler : IRequestHandler<SetVendorCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetVendorCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetVendorCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.ProductId))!;
            product.SetVendor(request.VendorId);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}