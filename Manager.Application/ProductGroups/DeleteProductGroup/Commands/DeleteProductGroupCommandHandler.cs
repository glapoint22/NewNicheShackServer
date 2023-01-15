using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.ProductGroups.DeleteProductGroup.Commands
{
    public sealed class DeleteProductGroupCommandHandler : IRequestHandler<DeleteProductGroupCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteProductGroupCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteProductGroupCommand request, CancellationToken cancellationToken)
        {
            ProductGroup productGroup = (await _dbContext.ProductGroups.FindAsync(request.Id))!;

            _dbContext.ProductGroups.Remove(productGroup);
            await _dbContext.SaveChangesAsync();

            List<Website.Domain.Entities.ProductInProductGroup> productsInProductGroup = await _websiteDbContext.ProductsInProductGroup
                .Where(x => x.ProductGroupId == request.Id)
                .ToListAsync();

            _websiteDbContext.ProductsInProductGroup.RemoveRange(productsInProductGroup);
            await _websiteDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}