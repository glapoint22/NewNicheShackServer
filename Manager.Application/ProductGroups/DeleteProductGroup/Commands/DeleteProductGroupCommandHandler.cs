using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.DeleteProductGroup.Commands
{
    public sealed class DeleteProductGroupCommandHandler : IRequestHandler<DeleteProductGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteProductGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteProductGroupCommand request, CancellationToken cancellationToken)
        {
            ProductGroup productGroup = (await _dbContext.ProductGroups.FindAsync(request.Id))!;

            _dbContext.ProductGroups.Remove(productGroup);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}