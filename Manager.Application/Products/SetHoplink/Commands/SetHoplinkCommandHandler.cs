using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetHoplink.Commands
{
    public sealed class SetHoplinkCommandHandler : IRequestHandler<SetHoplinkCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetHoplinkCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetHoplinkCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.SetHoplink(request.Hoplink);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}