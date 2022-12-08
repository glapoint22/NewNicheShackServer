using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetDescription.Commands
{
    public sealed class SetDescriptionCommandHandler : IRequestHandler<SetDescriptionCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetDescriptionCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetDescriptionCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.SetDescription(request.Description);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}