using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.AddProductGroup.Commands
{
    public sealed class AddProductGroupCommandHandler : IRequestHandler<AddProductGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddProductGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddProductGroupCommand request, CancellationToken cancellationToken)
        {
            ProductGroup productGroup = ProductGroup.Create(request.Name);

            _dbContext.ProductGroups.Add(productGroup);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(productGroup.Id);
        }
    }
}