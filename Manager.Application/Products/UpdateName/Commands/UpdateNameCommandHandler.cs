using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdateName.Commands
{
    public sealed class UpdateNameCommandHandler : IRequestHandler<UpdateNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateNameCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.UpdateName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}