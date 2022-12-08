using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetShipping.Commands
{
    public sealed class SetShippingCommandHandler : IRequestHandler<SetShippingCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetShippingCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetShippingCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.SetShipping(request.ShippingType);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}