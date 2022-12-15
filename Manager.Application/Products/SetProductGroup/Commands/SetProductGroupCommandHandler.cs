using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductGroup.Commands
{
    public sealed class SetProductGroupCommandHandler : IRequestHandler<SetProductGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetProductGroupCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetProductGroupCommand request, CancellationToken cancellationToken)
        {
            ProductInProductGroup productInProductGroup;

            if (request.Checked)
            {
                productInProductGroup = ProductInProductGroup.Create(request.ProductId, request.ProductGroupId);
                _dbContext.ProductsInProductGroup.Add(productInProductGroup);
            }
            else
            {
                productInProductGroup = await _dbContext.ProductsInProductGroup
                    .Where(x => x.ProductId == request.ProductId && x.ProductGroupId == request.ProductGroupId)
                    .SingleAsync();

                _dbContext.ProductsInProductGroup.Remove(productInProductGroup);
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}