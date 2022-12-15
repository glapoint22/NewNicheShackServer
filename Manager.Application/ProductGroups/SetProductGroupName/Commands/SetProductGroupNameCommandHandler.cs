using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.SetProductGroupName.Commands
{
    public sealed class SetProductGroupNameCommandHandler : IRequestHandler<SetProductGroupNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetProductGroupNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetProductGroupNameCommand request, CancellationToken cancellationToken)
        {
            ProductGroup productGroup = (await _dbContext.ProductGroups.FindAsync(request.Id))!;

            productGroup.SetName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}